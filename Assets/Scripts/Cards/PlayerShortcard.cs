using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerShortcard : MonoBehaviour
{
    private PlayerCardInfo playerInfo;
    
    [SerializeField] private Text playerName = null;
    [SerializeField] private EventTrigger openShortcardButton = null;

    private void Start()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener( (eventData) => { UIManager.Instance.OpenPlayerShortcard(this); } );
        openShortcardButton.triggers.Add(entry);
    }

    public void FillShortcard(PlayerCardInfo info, int playerNumber)
    {
        playerInfo = info;
        playerName.text = $"Игрок №{playerNumber}";
    }

    public PlayerCardInfo GetShortcardInfo()
    {
        return playerInfo;
    }
}
