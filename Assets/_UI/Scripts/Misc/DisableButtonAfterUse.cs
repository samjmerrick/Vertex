using UnityEngine;
using UnityEngine.UI;

public class DisableButtonAfterUse : MonoBehaviour {

    private Button button;

    public void OnEnable()
    {
        GameController.GameEnd += Enable;
    }

    public void OnDisable()
    {
        GameController.GameEnd -= Enable;
    }

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void Enable()
    {
        button.interactable = true;
    }

    public void Disable()
    {
        button.interactable = false;
    }
}
