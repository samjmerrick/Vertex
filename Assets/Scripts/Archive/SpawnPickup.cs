using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour {

    public GameObject[] PickupOptions;
    public int chance;

    private bool isQuitting = false;

    public void OnDestroy()
    {
        if (!isQuitting)
        {
            int Chance = Random.Range(0, chance);

            if (Chance < PickupOptions.Length)
            {
                GameObject pickup = Instantiate(
                    PickupOptions[Chance], 
                    transform.position, 
                    Quaternion.Euler(0, 0, 0));

                pickup.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -2, 0);
            }
        }
    }
    
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

}
