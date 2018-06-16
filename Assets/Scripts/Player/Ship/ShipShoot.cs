using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShoot : MonoBehaviour {

    // Buff Variables
    public GameObject bullet;
    public GameObject bigBullet;
    public GameObject laser;

    // Shooting Variables
    public float FireRate;
    private float lastFired;
    private float lastClickTime;
    private float catchTime = .25f;
    private bool shooting = true;

    private ShipBuffs buffs;

    private void Start()
    {
        buffs = GetComponent<ShipBuffs>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (GameController.GameRunning)
            CheckIfShooting();
    }

    void CheckIfShooting()
    {
        if (shooting)
        {
            if (buffs.Active.Contains("Rapid"))
            {
                if (Time.time - lastFired > (FireRate / 2))
                    Shoot();
            }
            else
                if (Time.time - lastFired > FireRate)
                Shoot();
        }

        // Laser
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            if (Time.time - lastClickTime < catchTime && !transform.Find("Laser(Clone)"))
            {
                if (Upgrades.Get()["Laser"] > 0)
                {
                    Instantiate(laser, transform);
                    shooting = false;
                    StartCoroutine(buffs.removeBuff("Laser", 5));
                    StartCoroutine(shootAgain(5));
                    Upgrades.Amend("Laser", -1);
                    UIControl.instance.Laser.text = Upgrades.Get("Laser").ToString();
                }
                else
                    UIControl.instance.UIMessage("Not enough lasers");
            }
            else
            {
                //normal click  
            }
            lastClickTime = Time.time;
        }
    }

    IEnumerator shootAgain(int time)
    {
        yield return new WaitForSeconds(time);
        shooting = true;
    }

    void Shoot()
    {
        GameObject toShoot = bullet;

        if (buffs.Active.Contains("Big"))
            toShoot = bigBullet;

        Instantiate(toShoot, new Vector3(transform.position.x, transform.position.y + 0.6f, 0), transform.rotation);

        if (buffs.Active.Contains("Triple"))
        {
            Instantiate(toShoot, new Vector3(transform.position.x + 0.2f, transform.position.y + 0.6f), Quaternion.Euler(0, 0, -5));
            Instantiate(toShoot, new Vector3(transform.position.x - 0.2f, transform.position.y + 0.6f), Quaternion.Euler(0, 0, 5));
        }

        lastFired = Time.time;
    }
}
