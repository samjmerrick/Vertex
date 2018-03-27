using System;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KillMission : Mission {

    public static List<string> EnemyNames = SpawnController.EnemyList;

    public KillMission()
    {
        Enemy.Death += EnemyCount;
        missionList.Add(this);
    }

    // Can be called from anywhere
    public static void KillRandom(int _toComplete)
    {
        int rand = Random.Range(0, EnemyNames.Count);
        string _nameOfObject = EnemyNames[rand] + "(Clone)";

        foreach (Mission missioninfo in GetMissions())
        {
            string currentName = missioninfo.NameOfObject;

            if (currentName == _nameOfObject)
            {
                RandomMissionGiver.RandomMission();
                return;
            }
        }

        string _objective = "DESTROY " + _toComplete + " " + EnemyNames[rand];

        KillMission mission = new KillMission();

        mission.objective = _objective;
        mission.toComplete = _toComplete;
        mission.NameOfObject = _nameOfObject;
    }

    // Counts down enemies
    private void EnemyCount(string EnemyDiedName, Vector3 pos)
    {
        if (NameOfObject == EnemyDiedName)
        {
            remaining++;

            if (toComplete == remaining)
            {
                FinishMission();
                Enemy.Death -= EnemyCount;
            }
        }
    }

    public override void StartListener()
    {
        Enemy.Death += EnemyCount;
    }

    public override void StopListener()
    { 
        Enemy.Death -= EnemyCount;
    }
}
