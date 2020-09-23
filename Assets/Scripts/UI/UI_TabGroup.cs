using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TabGroup : MonoBehaviour
{
    private UI_Panel parentPanel;
    
    [Header("Tab Control")]
    [SerializeField] private List<UI_TabButton> buttons = new List<UI_TabButton>();
    private UI_TabButton selectedTab;
    
    public Color SelectedColor;
    public Color DeselectedColor;
    
    [Header("Swipe Control")]
    public float swipeThreshold = 50f;
    public float timeThreshold = 0.3f;
    
    private Vector2 fingerDown;
    private DateTime fingerDownTime;
    private Vector2 fingerUp;
    private DateTime fingerUpTime;
    
    public Action OnSwipeLeft;
    public Action OnSwipeRight;

    private void Start()
    {
        parentPanel = GetComponentInParent<UI_Panel>();
        parentPanel.OnGetFocus += ResetTabs;
        SubscribeButtons();
        OnSwipeLeft += MoveToNextTab;
        OnSwipeRight += MoveToPreviousTab;
    }

    private void SubscribeButtons()
    {
        if(buttons.Count == 0)
            buttons.AddRange(GetComponentsInChildren<UI_TabButton>());
        if(buttons.Count > 0)
            OnTabSelected(buttons[0]);
    }
    
    /*
    public void Subscribe(UI_TabButton btn)
    {
        if (buttons == null)
            buttons = new List<UI_TabButton>();
        Debug.Log(btn.gameObject.name);
        buttons.Add(btn);
        if (!selectedTab)
            OnTabSelected(btn);
    }
    */

    public void OnTabSelected(UI_TabButton btn)
    {
        if(selectedTab != null)
            selectedTab.OnDeselect(DeselectedColor);
        selectedTab = btn;
        btn.OnSelect(SelectedColor);
    }

    private void MoveToNextTab()
    {
        int i = buttons.FindIndex(x => x == selectedTab);
        Debug.Log("Current tab: " + i);
        if (i < buttons.Count - 1)
        {
            selectedTab.OnDeselect(DeselectedColor);
            (selectedTab = buttons[i + 1]).OnSelect(SelectedColor);
        }
    }

    private void MoveToPreviousTab()
    {
        int i = buttons.FindIndex(x => x == selectedTab);
        Debug.Log("Current tab: " + i);
        if (i > 0)
        {
            selectedTab.OnDeselect(DeselectedColor);
            (selectedTab = buttons[i - 1]).OnSelect(SelectedColor);
        }
    }

    private void ResetTabs()
    {
        if (buttons.Count < 0)
            return;
        selectedTab.OnDeselect(DeselectedColor);
        (selectedTab = buttons[0]).OnSelect(SelectedColor);
    }

    private void Update()
    {
        if(parentPanel.IsHidden)
            return;
        
        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            fingerDown = Input.mousePosition;
            fingerUp = Input.mousePosition;
            fingerDownTime = DateTime.Now;
        }
        if (Input.GetMouseButtonUp(0))
        {
            fingerDown = Input.mousePosition;
            fingerUpTime = DateTime.Now;
            CheckSwipe();
        }
        #endif
        
        #if UNITY_ANDROID
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerDown = Input.mousePosition;
                fingerUp = Input.mousePosition;
                fingerDownTime = DateTime.Now;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = Input.mousePosition;
                fingerUpTime = DateTime.Now;
                CheckSwipe();
            }
        }
        #endif
    }

    private void CheckSwipe()
    {
        float duration = (float)fingerUpTime.Subtract(fingerDownTime).TotalSeconds;
        if (duration > timeThreshold)
            return;
        float deltaX = fingerDown.x - fingerUp.x;
        if (Mathf.Abs(deltaX) > swipeThreshold)
        {
            if (deltaX > 0)
            {
                OnSwipeRight?.Invoke();
                Debug.Log("Swipe Right");
            }
            else if (deltaX < 0)
            {
                OnSwipeLeft?.Invoke();
                Debug.Log("Swipe Left");
            }
        }
    }
    

}
