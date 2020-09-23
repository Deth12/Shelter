using System;
using UnityEngine;

public enum TransitionType
{
    None, Fade, LeftSlide, RightSlide, TopSlide, BottomSlide, Scale
}

public enum PanelState
{
    Hidden, Shown
}

public class UI_Panel : MonoBehaviour
{
    private CanvasGroup cg;
    private RectTransform rect;
    private Animator anim;

    [SerializeField] private PanelState state;
    
    //public bool IsHiddenByDefault = true;
    public bool IsHidden => state == PanelState.Hidden ? true : false;

    public Action OnLoseFocus;
    public Action OnGetFocus;
    
    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
        if (state == PanelState.Hidden)
            Hide(TransitionType.None);
    }

    public void SetPanelState(PanelState s)
    {
        state = s;
    }

    public void Hide(TransitionType t)
    {
        switch (t)
        {
            case TransitionType.None:
                anim.Play(AnimatorHashes.P_Hide);
                break;
            case TransitionType.Fade:
                anim.Play(AnimatorHashes.P_Disappear);
                break;
            case TransitionType.Scale:
                // TO IMPLEMENT;
                break;
            case TransitionType.LeftSlide:
                anim.Play(AnimatorHashes.P_SlideOutLeft);
                break;
            case TransitionType.RightSlide:
                anim.Play(AnimatorHashes.P_SlideOutRight);
                break;
            case TransitionType.TopSlide:
                anim.Play(AnimatorHashes.P_SlideOutTop);
                break;
            case TransitionType.BottomSlide:
                anim.Play(AnimatorHashes.P_SlideOutBottom);
                break;
        }
        OnLoseFocus?.Invoke();
    }

    public void Show(TransitionType t)
    {
        switch (t)
        {
            case TransitionType.None:
                anim.Play(AnimatorHashes.P_Show);
                break;
            case TransitionType.Fade:
                anim.Play(AnimatorHashes.P_Appear);
                break;
            case TransitionType.Scale:
                // TO IMPLEMENT;
                break;
            case TransitionType.LeftSlide:
                anim.Play(AnimatorHashes.P_SlideInLeft);
                break;
            case TransitionType.RightSlide:
                anim.Play(AnimatorHashes.P_SlideInRight);
                break;
            case TransitionType.TopSlide:
                anim.Play(AnimatorHashes.P_SlideInTop);
                break;
            case TransitionType.BottomSlide:
                anim.Play(AnimatorHashes.P_SlideInBottom);
                break;
        }
        OnGetFocus?.Invoke();
    }
}
