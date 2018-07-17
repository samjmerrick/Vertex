using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DistanceMission : Mission {

    private List<int> missionOptions = new List<int> { 200, 500, 1000 };

    public DistanceMission()
    {
        int missionChoice = Random.Range(0, missionOptions.Count);
        toComplete = missionOptions[missionChoice];
        reward = (missionChoice + 1) * 100;

        NameOfObject = "distance";
        objective = "TRAVEL " + toComplete + " Lightyears";
    }

    private void CheckDistance(int dist)
    {
        progress += dist;

        if (toComplete == progress)
        {
            FinishMission();
        }
    }

    public override void StartListener()
    {
        GameController.Distance += CheckDistance;
    }

    public override void StopListener()
    {
        GameController.Distance -= CheckDistance;
    }
}
