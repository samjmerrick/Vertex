using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShootMission : Mission {

    private List<string> MissionOptions = new List<string>() { "Close Call", "Long Shot" };

    public ShootMission()
    {
        NameOfObject = MissionOptions[Random.Range(0, MissionOptions.Count)];
        toComplete = 5;
        reward = 300;
        objective = "SHOOT " + toComplete + " " + NameOfObject;
    }

    private void ShootCount(float dist)
    {
        if (dist < 10 && NameOfObject == "Close Call")
            progress++;

        if (dist > 60 && NameOfObject == "Long Shot")
            progress++;

        if (progress == toComplete)
            FinishMission();
    }

	public override void StartListener()
    {
        PlayerBullet.Collided += ShootCount;
    }

    public override void StopListener()
    {
        PlayerBullet.Collided -= ShootCount;
    }
}
