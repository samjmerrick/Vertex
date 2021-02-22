using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class PlayerName : MonoBehaviour
{
    void Start()
    {
        Debug.Log("setting name to" + PlayerPrefs.GetString("playerName", "Your name"));
        GetComponent<InputField>().text = PlayerPrefs.GetString("playerName", "Your name");
    }

    public void UpdatePlayerName(string newName)
    {
        PlayerPrefs.SetString("playerName", newName);
        DatabaseManager.SaveScoreToDatabase();
    }
}

