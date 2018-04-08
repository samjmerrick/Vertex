using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * -100);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
            Destroy(gameObject);
            UIControl.Instance.UpdateCounter("Coins", PlayerPrefs.GetInt("Coins"));
        }
    }
}
