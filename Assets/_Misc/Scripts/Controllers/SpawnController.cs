using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public static int EnemiesRemaining;
    public float NewEnemyTime;
    public Vector2 SpawnTime;
    public int PickupChance;
    public int EnemyQuantityToSpawn = 5;
    public int TimeToAddDifficulty = 10;

    private Vector2 bounds;
    private int spawnChoice;
    private int availableEnemies = 3;

    // Enemies
    public GameObject[] Enemies, Pickups;

    void OnEnable()
    {
        GameController.GameBegin += StartGame;
        GameController.GameEnd += EndGame;
        Enemy.Death += EnemyDied;

        bounds = Bounds.Get();
    }

    void OnDisable()
    {
        GameController.GameBegin -= StartGame;
        GameController.GameEnd -= EndGame;
        Enemy.Death -= EnemyDied;
    }

    void StartGame()
    {
        EnemiesRemaining = 0;
        InvokeRepeating("AddNewEnemy", NewEnemyTime, NewEnemyTime);
        InvokeRepeating("AddToDifficulty", TimeToAddDifficulty, TimeToAddDifficulty);
        StartCoroutine(Spawn());
    }

    void AddToDifficulty()
    {
        EnemyQuantityToSpawn++;
        SpawnTime.y -= 0.1f;
    }

    void ChooseEnemyToSpawn()
    {
        spawnChoice = Random.Range(0, availableEnemies);
    }

    void AddNewEnemy()
    {
        availableEnemies++;

        if (availableEnemies == Enemies.Length)
            CancelInvoke("AddNewEnemy");
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(SpawnTime.x, SpawnTime.y));

            if (EnemiesRemaining < EnemyQuantityToSpawn)
            {
                GameObject EnemyToSpawn = Enemies[spawnChoice];
                Vector3 Position;

                if (!EnemyToSpawn.name.Contains("Group"))
                {
                    Position = new Vector3(
                        x: Random.Range(-bounds.x, bounds.x),
                        y: bounds.y);
                }

                else
                {
                    Position = EnemyToSpawn.transform.position;
                }

                Instantiate(Enemies[spawnChoice], Position, Enemies[spawnChoice].transform.rotation);
                ChooseEnemyToSpawn();
            }
        }
    }

    void EndGame()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy.gameObject);

        foreach (GameObject enemyFire in GameObject.FindGameObjectsWithTag("EnemyFire"))
            Destroy(enemyFire.gameObject);

        CancelInvoke();
        StopAllCoroutines();
    }


    void EnemyDied(string name, Vector3 pos)
    {
        int Chance = Random.Range(0, PickupChance);

        if (Chance < Pickups.Length)
        {
            GameObject pickup = Instantiate(
                Pickups[Chance],
                pos,
                Quaternion.identity);

            pickup.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -2, 0);
        }
    }
}
