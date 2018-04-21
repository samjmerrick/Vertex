using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickupMission : Mission {

    private static List<string> PickupNames = SpawnController.PickupList;
    private List<int> missionOptions = new List<int> {2, 3, 5};

    public PickupMission()
    {
        int missionChoice = Random.Range(0, missionOptions.Count);
        toComplete = missionOptions[missionChoice];
        reward = (missionChoice + 1) * 100;

        int rand = Random.Range(0, PickupNames.Count);
        NameOfObject = PickupNames[rand];
        objective = "PICKUP " + toComplete + " " + PickupNames[rand];
    }

    // Counts down enemies
    private void PickupCount(string PickedupName, int time)
    {
        if (NameOfObject == PickedupName)
        {
            progress++;

            if (toComplete == progress)
            {
                FinishMission();
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
