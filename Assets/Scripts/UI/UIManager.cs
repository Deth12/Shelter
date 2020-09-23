using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);
        Instance = this;
    }

    [SerializeField] private UI_Panel menuPanel = null;
    [SerializeField] private UI_Panel helpPanel = null;
    [SerializeField] private UI_Panel newGamePanel = null;
    [SerializeField] private UI_Panel gamePanel = null;
    [SerializeField] private UI_Panel bottomPanel = null;
    [SerializeField] private UI_Panel notificationPanel = null;
    [SerializeField] private UI_Panel playerCardPanel = null;
    
    [SerializeField] private UI_Toggle audioToggle = null;
    
    [SerializeField] private Slider playersSlider = null;

    [SerializeField] private UI_Button endGameButton = null;
    [SerializeField] private UI_Button closeShortcardButton = null;
    
    [Header("Game Elements")]
    [SerializeField] public CatastropheCard catastropheCard;
    [SerializeField] public BunkerCard bunkerCard;

    [SerializeField] public Transform playersContainer;
    [SerializeField] public PlayerShortcard shortcardPrefab;
    [SerializeField] public PlayerCard playerCard;

    
    public Action<int> OnGameStart;
    public Action OnGameEnd;
    
    private void Start()
    {
        menuPanel.gameObject.SetActive(true);
        helpPanel.gameObject.SetActive(true);
        newGamePanel.gameObject.SetActive(true);
        gamePanel.gameObject.SetActive(true);
        bottomPanel.gameObject.SetActive(true);
        notificationPanel.gameObject.SetActive(true);
        playerCardPanel.gameObject.SetActive(true);
        
        audioToggle.OnToggle += AudioManager.Instance.MuteAudio;

        helpPanel.OnGetFocus += () => bottomPanel.Show(TransitionType.None);
        helpPanel.OnLoseFocus += () => bottomPanel.Hide(TransitionType.None);

        gamePanel.OnGetFocus += () => bottomPanel.Show(TransitionType.None);
        gamePanel.OnLoseFocus += () => bottomPanel.Hide(TransitionType.None);
    }

    public void OpenHelpPanel()
    {
        HidePanel(menuPanel, TransitionType.BottomSlide);
        helpPanel.Show(TransitionType.TopSlide);
    }

    public void OpenNewGamePanel()
    {
        HidePanel(menuPanel, TransitionType.LeftSlide);
        if(GameManager.Instance.IsGameInProgress)
            gamePanel.Show(TransitionType.RightSlide);
        else
            newGamePanel.Show(TransitionType.RightSlide);
    }

    public void OpenGamePanel()
    {
        if(!newGamePanel.IsHidden)
            newGamePanel.Hide(TransitionType.LeftSlide);
        gamePanel.Show(TransitionType.RightSlide);
        endGameButton.gameObject.SetActive(true);
        OnGameStart?.Invoke((int)playersSlider.value);
    }

    public void CloseGamePanel(bool endCurrentGame)
    {
        if (endCurrentGame)
        {
            Notification.Instance.Show(
                "Вы действительно хотите завершить текущую игру?",
                () => 
                {
                    ReturnToMenu();
                    OnGameEnd?.Invoke();
                });
        }
        else
            ReturnToMenu();
    }

    public void ReturnToMenu()
    {
        HidePanel(helpPanel, TransitionType.None);
        HidePanel(newGamePanel, TransitionType.None);
        HidePanel(gamePanel, TransitionType.None);
        ClosePlayerShortcard();
        endGameButton.gameObject.SetActive(false);
        menuPanel.Show(TransitionType.Fade);
    }

    public void OpenPlayerShortcard(PlayerShortcard s)
    {
        playerCardPanel.Show(TransitionType.Fade);
        closeShortcardButton.gameObject.SetActive(true);
        playerCard.FillPlayerCard(s.GetShortcardInfo());
    }

    public void ClosePlayerShortcard()
    {
        closeShortcardButton.gameObject.SetActive(false);
        HidePanel(playerCardPanel, TransitionType.Fade);
    }

    public void HidePanel(UI_Panel panel, TransitionType t)
    {
        if(!panel.IsHidden)
            panel.Hide(t);
    }
}
