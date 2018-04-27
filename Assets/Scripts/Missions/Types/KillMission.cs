using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KillMission : Mission {

    public KillMission()
    {
        GameObject[] Enemies = Object.FindObjectOfType<SpawnController>().Enemies;

        Enemy target = Enemies[Random.Range(0, Enemies.Length)].GetComponent<Enemy>();

        toComplete = 20;
        reward = 200;
        NameOfObject = target.name;
        objective = "DESTROY " + toComplete + " " + NameOfObject;
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
