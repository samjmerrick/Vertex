using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class PlayerName : MonoBehaviour
{
    void Start()
    {
        Debug.Log("setting name to" + PlayerPrefs.GetString("playerName", "Enter your name"));
        GetComponent<InputField>().text = PlayerPrefs.GetString("playerName", "Enter your name");
    }

    public void UpdatePlayerName(string newName)
    {
        Debug.Log("updating name to " + newName);
        PlayerPrefs.SetString("playerName", newName);
        DatabaseManager.SaveScoreToDatabase();
    }
}

