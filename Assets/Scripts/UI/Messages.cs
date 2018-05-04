using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messages : MonoBehaviour {

    public Text MessageText;

    private void OnEnable()
    {
        Mission.MissionComplete += MissionMessage;
    }

    private void OnDisable()
    {
        Mission.MissionComplete -= MissionMessage;
    }

    public Text NewMessage(string message)
    {
        Text newMessage = Instantiate(MessageText, transform);
        newMessage.text = message;

        // Destroy when animation finishes
        Destroy(newMessage.gameObject, newMessage.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        return newMessage;
    }

    public void NewMessage(string message, Color32 color)
    {
        Text newMessage = NewMessage(message);
        newMessage.color = color;
    }

    void MissionMessage(Mission mission)
    {
        NewMessage("Completed: " + mission.GetObjective(), new Color(255, 140, 0));
    }
}
