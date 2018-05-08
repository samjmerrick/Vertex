using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionPrefab : MonoBehaviour {

    public Mission mission;

    void Start()
    {
        if (mission != null)
        {
            int progress = mission.ReturnProgress();
            int toComplete = mission.ReturntoComplete();

            // Update Objective
            Text objective = transform.Find("Objective").gameObject.GetComponent<Text>();
            objective.text = mission.GetObjective() + " (" + progress + "/" + toComplete + ")";

            Text reward = transform.Find("Reward").gameObject.GetComponent<Text>();
            reward.text = mission.reward.ToString();

            // Update image
            Image image = transform.Find("MissionImage").gameObject.GetComponent<Image>();

            if (Resources.Load("Missions/" + mission.NameOfObject) != null)
                image.sprite = (Sprite)Resources.Load("Missions/" + mission.NameOfObject, typeof(Sprite));
   
        }
    }

    public void SkipMission()
    {
        RandomMissionGiver randomMissionGiver = FindObjectOfType<RandomMissionGiver>();
        randomMissionGiver.SkipMission(mission);

        transform.parent.GetComponent<MissionsList>().ShowMissions();
       
    }

}
