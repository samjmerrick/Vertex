using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionPrefab : MonoBehaviour {

    [HideInInspector]
    public Mission mission;

    public Text objective;
    public Text reward;
    public Image image;

    void Start()
    {
        if (mission != null)
        {
            int progress = mission.progress;
            int toComplete = mission.toComplete;

            objective.text = mission.objective + " (" + progress + "/" + toComplete + ")";
            reward.text = mission.reward.ToString();

            if (Resources.Load("Missions/" + mission.NameOfObject) != null)
                image.sprite = (Sprite)Resources.Load("Missions/" + mission.NameOfObject, typeof(Sprite));
   
        }
    }

    public void SkipMission()
    {
        RandomMissionGiver randomMissionGiver = FindObjectOfType<RandomMissionGiver>();
        randomMissionGiver.SkipMission(mission);

        GetComponentInParent<MissionsList>().ShowMissions();
    }
}
