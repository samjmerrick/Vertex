using UnityEngine;

public class PlayAudioOnDestroy : MonoBehaviour {

    private void OnDestroy()
    {
        AudioManager.PlaySound(GetComponent<AudioSource>().clip);
    }
}
