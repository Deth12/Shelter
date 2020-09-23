using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);
        Instance = this;
    }

    private List<PlayerShortcard> shortcards = new List<PlayerShortcard>();
    public bool IsGameInProgress { get; private set; }
    
    private void Start()
    {
        UIManager.Instance.OnGameStart += NewGame;
        UIManager.Instance.OnGameEnd += EndGame;
    }

    private void NewGame(int playersAmount)
    {
        if (!IsGameInProgress)
        {
            UIManager.Instance.catastropheCard.FillCatastropheCard(Generator.GenerateCatastrophy());
            UIManager.Instance.bunkerCard.FillBunkerCard(Generator.GenerateBunker());
            for (int i = 0; i < playersAmount; i++)
            {
                PlayerShortcard sc = 
                    Instantiate(UIManager.Instance.shortcardPrefab, UIManager.Instance.playersContainer);
                sc.FillShortcard(Generator.GeneratePlayerCard(), i + 1);
                shortcards.Add(sc);
            }
            IsGameInProgress = true;
        }
    }

    private void EndGame()
    {
        foreach (PlayerShortcard sc in shortcards)
            Destroy(sc.gameObject);
        shortcards = new List<PlayerShortcard>();
        IsGameInProgress = false;
    }

    private void OnDisable()
    {
        UIManager.Instance.OnGameStart -= NewGame;
    }
}
