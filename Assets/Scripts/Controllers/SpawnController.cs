using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public float SpawnRate;
    public float SwitchTime;
    public int PickupChance;
    public static int toSpawn = 3;
    public static int EnemiesRemaining;
    private int spawnChoice;

    // Enemies
    public GameObject[] Enemies;
    public static List<string> EnemyList = new List<string>();

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
        Enemy.Death += SpawnPickup;
    }

    void OnDisable()
    {
        EnemyList.Clear();
        PickupList.Clear();

        GameController.GameBegin -= StartGame;
        GameController.GameEnd -= EndGame;
        Enemy.Death -= SpawnPickup;
    }

    void StartGame()
    {
        EnemiesRemaining = 0;
        InvokeRepeating("ChangeSpawn", 0, SwitchTime);
        InvokeRepeating("Spawn", 3, SpawnRate);
    }

    void EndGame()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
            Destroy(enemy.gameObject);

        CancelInvoke();
    }

    void Spawn()
    {
        if (EnemiesRemaining < toSpawn)
        {
            Vector3 location = new Vector3(
                x: Random.Range(-2.5f, 2.5f),
                y: 5f);

            Instantiate(Enemies[spawnChoice], location, Quaternion.identity); 
        }
    }

    void ChangeSpawn()
    {
        spawnChoice = Random.Range(0, Enemies.Length);
    }

    void SpawnPickup(string name, Vector3 pos)
    {
        int Chance = Random.Range(0, PickupChance);

        if (Chance < Pickups.Length)
        {
            GameObject pickup = Instantiate(
                Pickups[Chance],
                pos,
                Quaternion.Euler(0, 0, 0));

            pickup.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -2, 0);
        }
    }
}
