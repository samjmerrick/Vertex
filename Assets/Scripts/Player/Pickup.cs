using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    private string thisName;

    public delegate void GotDelegate (string name, int time);
    public static event GotDelegate Got;

    public int Time = 3;
 
    private void Awake()
    {
        thisName = name.Replace("(Clone)", "");

        ShipBuffs shipBuffs = FindObjectOfType<ShipBuffs>();
        if (shipBuffs.Active.Contains(thisName))
            Destroy(gameObject);

        int level = Upgrades.Get(thisName);
        
        Time += (level * 2);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player" && c.gameObject.name != ("Shield(Clone)"))
        {
            Destroy(gameObject);

            // Error check delegate subscriptions
            if (Got != null)
                Got(thisName, Time);
        }
    }
}
