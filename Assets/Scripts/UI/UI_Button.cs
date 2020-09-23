using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    //public bool Interactable;
    [SerializeField] private RectTransform rect = null;
    
    [Header("Colors")]
    public Color NormalColor;
    public Color PressedColor;
    public float TransitionTime = 0.1f;
    
    [Header("Events")]
    public bool Interactable = true;
    public UnityEvent OnClick;
    
    private void Start()
    {
        if(rect == null)
            rect = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
        LeanTween.color(rect, PressedColor, TransitionTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer Up");
        LeanTween.color(rect, NormalColor, TransitionTime);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(AudioManager.Instance != null)
            AudioManager.Instance.ButtonClickSound();
        if(Interactable)
            OnClick?.Invoke();
    }
}
