using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public static int EnemiesRemaining;
    public float SpawnRate, SwitchTime, NewEnemyTime;
    public Vector2 BossTime;
    public int PickupChance;
    [HideInInspector]
    public int toSpawn = 3;

    private Vector3 bounds;
    private int spawnChoice, availableEnemies;

    // Enemies
    public GameObject[] Enemies;
    public static List<string> EnemyList = new List<string>();

    // Bosses
    public GameObject[] Bosses;

    // Pickups
    public GameObject[] Pickups;
    public static List <string> PickupList= new List<string>();

    void OnEnable()
    {
        foreach (GameObject Enemy in Enemies)
            if (!Enemy.name.Contains("_"))
                EnemyList.Add(Enemy.name);

        foreach (GameObject Pickup in Pickups)
            if (!Pickup.name.Contains("_"))
                PickupList.Add(Pickup.name);

        GameController.GameBegin += StartGame;
        GameController.GameEnd += EndGame;
        Enemy.Death += EnemyDied;

        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        bounds.x += .5f;
    }

    void OnDisable()
    {
        EnemyList.Clear();
        PickupList.Clear();

        GameController.GameBegin -= StartGame;
        GameController.GameEnd -= EndGame;
        Enemy.Death -= EnemyDied;
    }

    void StartGame()
    {
        EnemiesRemaining = 0;
        InvokeRepeating("ChangeSpawn", 0, SwitchTime);
        InvokeRepeating("Spawn", 3, SpawnRate);
        InvokeRepeating("AddNewEnemy", NewEnemyTime, NewEnemyTime);
        StartCoroutine(SpawnBoss());
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

    void Spawn()
    {
        if (EnemiesRemaining < toSpawn)
        {
            Vector3 location = new Vector3(
                x: Random.Range(-bounds.x, bounds.x),
                y: bounds.y);

            Instantiate(Enemies[spawnChoice], location, Quaternion.identity);
            EnemiesRemaining++;
        }
    }

    IEnumerator SpawnBoss()
    {
        float WaitTime = Random.Range(BossTime.x, BossTime.y);
        yield return new WaitForSeconds(WaitTime);

        int choice = Random.Range(0, Bosses.Length);
        Instantiate(Bosses[choice], new Vector2(0, bounds.y), Quaternion.identity);
    }

    void ChangeSpawn()
    {
        spawnChoice = Random.Range(0, availableEnemies);
    }

    void AddNewEnemy()
    {
        availableEnemies++;

        if (availableEnemies == Enemies.Length)
            CancelInvoke("AddNewEnemy");
    }

    void EnemyDied(string name, Vector3 pos)
    {
        EnemiesRemaining--;
        int Chance = Random.Range(0, PickupChance);

        if (Chance < Pickups.Length)
        {
            GameObject pickup = Instantiate(
                Pickups[Chance],
                pos,
                Quaternion.identity);

            pickup.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -2, 0);
        }

        if (name.Contains("Boss"))
            StartCoroutine(SpawnBoss());   
    }
}
