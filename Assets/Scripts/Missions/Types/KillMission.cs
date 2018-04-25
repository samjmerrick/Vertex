using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KillMission : Mission {

    private List<string> EnemyNames = SpawnController.EnemyList;

    public KillMission()
    {
        toComplete = 20;
        reward = 200;

        EnemyNames.Add("Boss");
            

        int rand = Random.Range(0, EnemyNames.Count);
        NameOfObject = EnemyNames[rand];
        objective = "DESTROY " + toComplete + " " + EnemyNames[rand];

       
    }

    // Counts down enemies
    private void EnemyCount(string EnemyDiedName, Vector3 pos)
    {
        if (EnemyDiedName.Contains(NameOfObject))
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
        Enemy.Death += EnemyCount;
    }

    public override void StopListener()
    { 
        Enemy.Death -= EnemyCount;
    }
}
