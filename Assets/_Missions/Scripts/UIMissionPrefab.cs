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
    public Slider ProgressBar;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        SetMission();
        CheckIfComplete();
    }

    void SetMission()
    {
        mission = Missions.missionList[transform.GetSiblingIndex()];

        if (mission != null)
        {   
            // Text
            reward.text = mission.reward.ToString();

            // Progress bar
            ProgressBar.maxValue = mission.toComplete;
            ProgressBar.value = mission.cacheProgress;

            if (mission.progress > mission.cacheProgress)
                StartCoroutine(MissionProgress());


            if (Resources.Load("Missions/" + mission.NameOfObject) != null)
            {
                image.sprite = (Sprite)Resources.Load("Missions/" + mission.NameOfObject, typeof(Sprite));
            }
            else
            {
                image.sprite = (Sprite)Resources.Load("Missions/Question_mark", typeof(Sprite));
            }
        }
    }

    IEnumerator MissionProgress()
    {
        while(ProgressBar.value <= mission.progress)
        {
            ProgressBar.value += 0.1f;
            yield return new WaitForFixedUpdate();

        }
    }

    private void Update()
    {
        if (mission != null)
        {
            // Update progress
            objective.text = mission.objective + " (" + mission.progress + "/" + mission.toComplete + ")";
        }  
    }

    void CheckIfComplete()
    {
        if (mission.progress >= mission.toComplete)
        {
            SetNewMission();
        }
    }

    public void SetNewMission()
    {
        anim.SetTrigger("NewMission");

        RandomMissionGiver randMissionGiver = FindObjectOfType<RandomMissionGiver>();
        randMissionGiver.ReplaceMission(mission);
    }

    public void SkipMission()
    {
        SetNewMission();
    }
}
