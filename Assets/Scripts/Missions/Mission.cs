using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Mission {

    protected static List<Mission> missionList = new List<Mission>();
    public static List<Mission> GetMissions() { return new List<Mission>(missionList); }

    public string NameOfObject;
    public int toComplete = 0;
    public int remaining = 0;
    public string objective;
    public int reward;

    public virtual void FinishMission()
    {
        missionList.Remove(this);
        UIControl.instance.FinishMission(this);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 100);
        UIControl.Instance.CoinUpdate(PlayerPrefs.GetInt("Coins"));

        RandomMissionGiver.RandomMission();
    }

    public virtual int ReturntoComplete()
    {
        return toComplete;
    }

    public virtual int ReturnRemaining()
    {
        return remaining;
    }

    public virtual string GetObjective()
    {
        return objective;
    }

    public abstract void StartListener();
    public abstract void StopListener();

    public static void LoadMissions(List<Mission> missions)
    {
        missionList.Clear();
        missionList = missions;

        foreach (Mission mission in missionList)
            mission.StartListener();
    }
}

