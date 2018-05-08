using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Animator))]

public class Panel : MonoBehaviour {

    public Panel[] ShowBeforeLoading;

    private Animator _animator;
    private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        PanelManager.ChangePanel += CheckIfOpen;
    }

    private void OnDisable()
    {
        PanelManager.ChangePanel -= CheckIfOpen;
    }

    public bool isOpen
    {
        get { return _animator.GetBool("IsOpen"); }
        set { _animator.SetBool("IsOpen", value); }
    }

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasGroup = GetComponent<CanvasGroup>();

        var rect = GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = new Vector2(0, 0);
    }

    void CheckIfOpen(Panel panel)
    {
        if (panel == this)
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
            SetChildActiveState(true);
        }

        else
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
            SetChildActiveState(false);
        }   
    }

    void SetChildActiveState(bool isActive)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(isActive);
    }

}
