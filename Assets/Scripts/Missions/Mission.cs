using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Mission {

    public static List<Mission> missionList = new List<Mission>();
    public static List<Mission> GetMissions() { return new List<Mission>(missionList); }

    public delegate void Complete(Mission mission);
    public static event Complete MissionComplete;

    public string NameOfObject;
    public int toComplete = 0;
    public int progress = 0;
    public string objective;
    public int reward;
    public bool perGame;

    public virtual void FinishMission()
    {
        if (!missionList.Contains(this))
            throw new ArgumentException("Finished Mission is not in the MissionList");

        MissionComplete(this);

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 100);
        UIControl.Instance.Coins.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public virtual int ReturntoComplete()
    {
        return toComplete;
    }

    public virtual int ReturnProgress()
    {
        return progress;
    }

    public virtual string GetObjective()
    {
        return objective;
    }

    public abstract void StartListener();
    public abstract void StopListener();

    public static void LoadMissions(List<Mission> missions)
    {
        foreach (Mission mission in missionList)
            mission.StopListener();

        missionList.Clear();
        missionList = missions;

        foreach (Mission mission in missionList)
        {
            mission.StartListener();

            // If this is a Per-Game Mission, progress = 0
            if (mission.perGame)
            {
                mission.progress = 0;
            }  
        }   
    }

    public static List<Mission> ClearMissions()
    {
        List<Mission> clearedMissions = new List<Mission>();

        foreach (Mission mission in missionList)
        {
            if (mission.progress == mission.toComplete)
            {
                missionList.Remove(mission);
                clearedMissions.Add(mission);
            }
        }

        return clearedMissions;
    }
}


