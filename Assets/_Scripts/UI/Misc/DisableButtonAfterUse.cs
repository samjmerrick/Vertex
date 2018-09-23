using UnityEngine;
using UnityEngine.UI;

public class DisableButtonAfterUse : MonoBehaviour {

	public void Disable()
    {
        GetComponent<Button>().interactable = false;
    }
}
