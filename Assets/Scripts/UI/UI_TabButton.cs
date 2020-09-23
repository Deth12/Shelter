using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UI_TabButton : MonoBehaviour, IPointerClickHandler
{
    private UI_TabGroup tabGroup;
    [SerializeField] private Image img = null;
    [SerializeField] private GameObject tabPage = null;

    private void Awake()
    {
        img = GetComponent<Image>();
        tabGroup = GetComponentInParent<UI_TabGroup>();
        tabPage.gameObject.SetActive(false);
    }

    public void OnSelect(Color selectedColor)
    {
        img.color = selectedColor;
        tabPage.gameObject.SetActive(true);        
    }

    public void OnDeselect(Color deselectedColor)
    {
        img.color = deselectedColor;
        tabPage.gameObject.SetActive(false);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }
}
