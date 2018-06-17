using System;
using UnityEngine;

[System.Serializable]
public abstract class Mission {

    public string NameOfObject, objective;
    public int toComplete, progress, reward = 0;
    public bool perGame;

    public delegate void Complete(Mission mission);
    public static event Complete MissionComplete;

    public abstract void StartListener();
    public abstract void StopListener();

    public virtual void FinishMission()
    {
        StopListener();

        if (!Missions.missionList.Contains(this))
            throw new ArgumentException("Finished Mission is not in the MissionList");

        MissionComplete(this);

        Coins.Add(reward);
        UIControl.Instance.CoinsText.text = Coins.Get().ToString();
    }

    public void CheckOneTime()
    {
        // Chance to be a per-game mission
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            perGame = true;
            objective += " in one game";
            reward *= 2;
        }
    }
}




