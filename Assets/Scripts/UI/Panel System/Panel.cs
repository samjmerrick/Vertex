using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Animator))]

public class Panel : MonoBehaviour {

    private Animator _animator;
    private CanvasGroup _canvasGroup;

    bool isOpen
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

        gameObject.SetActive(false);
    }


    public void SetActive(bool value)
    {
        _canvasGroup.blocksRaycasts = _canvasGroup.interactable = value;
        gameObject.SetActive(value);

        if (value == true)
            isOpen = value;
    }
}
