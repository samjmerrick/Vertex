using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messages : MonoBehaviour {

    public GameObject message;

    private void OnEnable()
    {
        Mission.MissionComplete += MissionMessage;
    }

    private void OnDisable()
    {
        Mission.MissionComplete -= MissionMessage;
    }

    public void Message(string m)
    {
        GameObject go = Instantiate(message, transform);
        go.GetComponent<Text>().text = m;

        // Destroy when animation finishes
        Destroy(go, go.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    void MissionMessage(Mission mission)
    {
        GameObject go = Instantiate(message, transform);
        go.GetComponent<Text>().text = "Completed: " + mission.GetObjective();
        go.GetComponent<Text>().color = new Color(255,140,0);

        // Destroy when animation finishes
        Destroy(go, go.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}
