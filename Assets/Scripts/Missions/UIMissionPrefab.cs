using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionPrefab : MonoBehaviour {

    public Mission mission;
    private Text expireTime;

    void Start()
    {
        if (mission != null)
        {
            int progress = mission.ReturnProgress();
            int toComplete = mission.ReturntoComplete();

            // Update Objective
            Text objective = transform.Find("Objective").gameObject.GetComponent<Text>();
            objective.text = mission.GetObjective() + " (" + progress + "/" + toComplete + ")";   

            // Update image
            Image image = transform.Find("MissionImage").gameObject.GetComponent<Image>();
            image.sprite = (Sprite)Resources.Load("Buff_NoGlow/" + mission.NameOfObject, typeof(Sprite));
        }
    }

    public void ChangeMission()
    {
        Start();
    }
}
