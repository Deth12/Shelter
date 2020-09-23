using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Toggle : MonoBehaviour, IPointerClickHandler
{
    public bool IsOn = true;

    private Image img;
    
    [SerializeField] private Sprite onSprite = null;
    [SerializeField] private Sprite offSprite = null;

    public Action<bool> OnToggle;

    public void Start()
    {
        img = GetComponent<Image>();
        img.sprite = IsOn ? onSprite : offSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(AudioManager.Instance != null)
            AudioManager.Instance.ButtonClickSound();
        IsOn = !IsOn;
        img.sprite = IsOn ? onSprite : offSprite;
        OnToggle?.Invoke(IsOn);    
    }
}
