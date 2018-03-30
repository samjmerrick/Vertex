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

    void MissionMessage(Mission mission)
    {
        GameObject go = Instantiate(message, transform);
        go.GetComponent<Text>().text = "Completed: " + mission.GetObjective();

        // Destroy when animation finishes
        Destroy(go, go.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}
