using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickupMission : Mission {

    private List<int> missionOptions = new List<int> {2, 3, 5};

    public PickupMission()
    {
        GameObject[] Pickups = Object.FindObjectOfType<SpawnController>().Pickups;
        Pickup target = Pickups[Random.Range(0, Pickups.Length)].GetComponent<Pickup>();

        int missionChoice = Random.Range(0, missionOptions.Count);
        toComplete = missionOptions[missionChoice];
        reward = (missionChoice + 1) * 100;

        NameOfObject = target.name;
        objective = "PICKUP " + toComplete + " " + NameOfObject;

        if (missionChoice == 0)
            CheckOneTime();
    }

    // Counts down enemies
    private void PickupCount(string PickedupName, int time)
    {
        if (PickedupName.Contains(NameOfObject))
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
