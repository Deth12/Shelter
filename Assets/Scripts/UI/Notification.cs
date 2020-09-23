using System;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public enum PromptResult
    {
        Cancel, Confirm    
    }
    
    public static Notification Instance;
    private void Awake()
    {
        Instance = this;
    }
    
    private UI_Panel notificationPanel;
    [SerializeField] private Text notificationText = null;

    private Action onConfirm;

    private void Start()
    {
        this.gameObject.SetActive(true);
        notificationPanel = GetComponent<UI_Panel>();
    }

    public void Show(string text, Action onConfirmAction)
    {
        notificationPanel.Show(TransitionType.Fade);
        notificationText.text = text;
        onConfirm = onConfirmAction;
    }

    public void Hide()
    {
        notificationPanel.Hide(TransitionType.Fade);
    }

    public void Result(bool result)
    {
        Hide();
        switch ((PromptResult)(result ? 1 : 0))
        {
            case PromptResult.Confirm:
                onConfirm?.Invoke();           
                break;
            case PromptResult.Cancel:
                break;
        }
    }
}
