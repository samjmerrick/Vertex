using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KillMission : Mission {

    private List<int> missionOptions = new List<int> { 5, 10, 20 };

    public KillMission()
    {
        // Find SpawnController, choose our target
        GameObject[] Enemies = Object.FindObjectOfType<SpawnController>().Enemies;
        Enemy target = Enemies[Random.Range(0, Enemies.Length)].GetComponent<Enemy>();

        // This int should be a reference to missionOptions
        int missionChoice = Random.Range(0, missionOptions.Count);

        NameOfObject = target.name;
        toComplete = missionOptions[missionChoice];
        reward = 50 * (missionChoice + 1) * target.difficulty;
   
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
