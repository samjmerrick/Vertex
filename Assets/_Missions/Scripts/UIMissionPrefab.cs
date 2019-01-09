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

    private void OnEnable()
    {
        anim = GetComponent<Animator>();

        if (Missions.missionList.Count == 0) return; // Missions are not loaded yet
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
            float progress = (float)mission.cacheProgress / (float)mission.toComplete; // Normalised value
            ProgressBar.value = progress; 

            if (mission.progress > mission.cacheProgress)
                StartCoroutine(MissionProgress());

            // Image
            SetImage();
        }
    }

    void SetImage()
    {
        if (mission.GetType() == typeof(KillMission))
        {
            SpriteRenderer enemySprite = FindObjectOfType<SpawnController>().ReturnEnemy(mission.NameOfObject).GetComponent<SpriteRenderer>();

            image.sprite = enemySprite.sprite;
            image.color = enemySprite.color;
        }

        else
        {
            image.color = Color.white;
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
        float progress = (float)mission.progress / (float)mission.toComplete; // Normalised value
        float amountToIncrease = progress - ProgressBar.value;

        while (ProgressBar.value < progress)
        {
            ProgressBar.value += (amountToIncrease * 0.025f);
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
