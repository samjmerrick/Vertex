using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KillMission : Mission {

    private static List<string> EnemyNames = SpawnController.EnemyList;
  
    public KillMission()
    {
        int rand = Random.Range(0, EnemyNames.Count);
        toComplete = 20;
        NameOfObject = EnemyNames[rand] + "(Clone)";
        objective = "DESTROY " + toComplete + " " + EnemyNames[rand];
    }

    // Counts down enemies
    private void EnemyCount(string EnemyDiedName, Vector3 pos)
    {
        if (NameOfObject == EnemyDiedName)
        {
            progress++;

            if (toComplete == progress)
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
