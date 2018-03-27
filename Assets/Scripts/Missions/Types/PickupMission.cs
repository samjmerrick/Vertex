using System;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickupMission : Mission { 

    public static List<string> PickupNames = SpawnController.PickupList;

    public PickupMission()
    {
        Pickup.Got += PickupCount;
        missionList.Add(this);
    }

    // Can be called from anywhere
    public static void RandomPickupMission(int _toComplete)
    {
        int rand = Random.Range(0, PickupNames.Count);
        string _nameOfObject = PickupNames[rand];

        foreach (Mission missioninfo in GetMissions())
        {
            string currentName = missioninfo.NameOfObject;

            if (currentName == _nameOfObject)
            {
                RandomMissionGiver.RandomMission();
                return;
            }
        }

        string _objective = "PICKUP " + _toComplete + " " + PickupNames[rand];

        PickupMission mission = new PickupMission();

        mission.objective = _objective;
        mission.toComplete = _toComplete;
        mission.NameOfObject = _nameOfObject;
    }

    // Counts down enemies
    private void PickupCount(string PickedupName, int time)
    {
        if (NameOfObject == PickedupName)
        {
            remaining++;

            if (toComplete == remaining)
            {
                FinishMission();
                Pickup.Got -= PickupCount;
            }
        }
    }

    public override void StartListener()
    {
        Pickup.Got += PickupCount;
    }

    public override void StopListener()
    {
        Pickup.Got -= PickupCount;
    }

}
