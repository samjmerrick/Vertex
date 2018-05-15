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

    private Animator anim;

    void OnEnable()
    {
        anim = GetComponent<Animator>();
        
    }

    private void Start()
    {
        SetMission();
    }

    void SetMission()
    {
        mission = Missions.missionList[transform.GetSiblingIndex()];

        if (mission != null)
        {
            int progress = mission.progress;
            int toComplete = mission.toComplete;

            objective.text = mission.objective + " (" + progress + "/" + toComplete + ")";
            reward.text = mission.reward.ToString();

            if (Resources.Load("Missions/" + mission.NameOfObject) != null)
                image.sprite = (Sprite)Resources.Load("Missions/" + mission.NameOfObject, typeof(Sprite));

            if (progress >= toComplete && !GameController.instance.GameRunning)
            {
                StartCoroutine(SetNewMission());
            }
        }
    }

    public IEnumerator SetNewMission()
    {
        anim.SetTrigger("NewMission");

        RandomMissionGiver randMissionGiver = FindObjectOfType<RandomMissionGiver>();
        randMissionGiver.ReplaceMission(mission);

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        SetMission();        
    }

    public void SkipMission()
    {
        StartCoroutine(SetNewMission());
    }
}
