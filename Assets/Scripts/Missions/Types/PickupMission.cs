using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickupMission : Mission {

    private static List<string> PickupNames = SpawnController.PickupList;

    public PickupMission()
    {
        int rand = Random.Range(0, PickupNames.Count);
        toComplete = 3;
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
