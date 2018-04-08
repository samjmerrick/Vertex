using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour
{
    // Buff Variables
    public GameObject bullet;
    public GameObject shield;
    public GameObject laser;

    // Shooting Variables
    public float FireRate;
    private float lastFired;
    private float lastClickTime;
    private float catchTime = .25f;
    private bool shooting = true;

    // Movement Variables
    public int speed;
    private Vector3 target;
    private Vector3 moveStartInput;
    private Vector3 moveStartPos;
    private bool moving;

    // Camera movement
    private Vector2 velocity = Vector2.zero;
    private Camera gamecamera;
    private Vector3 bounds;
    public float exSpace = 0.5f;

    // Statics
    public static Dictionary<string, int> upgrades = new Dictionary<string, int>();
    public static List<string> ActiveBuffs = new List<string>();
    
    // Events
    public delegate void Died();
    public static event Died Death;

    void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        bounds.x += exSpace;

        ClearBuffs();
        target = transform.position;
        gamecamera = FindObjectOfType<Camera>();

        if (!upgrades.ContainsKey("Laser"))
            upgrades.Add("Laser", 1);
    }

    void Update()
    {
        Move();
        CheckIfShooting();
    }

    void Move()
    {
        if (Input.GetMouseButton(0))
        {
            // If we're not already moving, initialise movement
            if (moving == false)
            {
                moveStartInput = Input.mousePosition;
                moveStartPos = Camera.main.WorldToScreenPoint(transform.position);
                moving = true;
            }

            target = Camera.main.ScreenToWorldPoint((Input.mousePosition - moveStartInput) + moveStartPos);
            target.x = Mathf.Clamp(target.x, -bounds.x, bounds.x);
            target.y = Mathf.Clamp(target.y, -bounds.y, bounds.y);

            transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);

            // Camera Movement
            float diff = target.x - transform.position.x;
            Vector3 gcPos = gamecamera.transform.position;

            gcPos.x = Mathf.SmoothDamp(gcPos.x, gcPos.x + diff, ref velocity.x, 0.4f);
            gcPos.x = Mathf.Clamp(gcPos.x, -bounds.x, bounds.x);

            gamecamera.transform.position = gcPos;
        }
        else
            moving = false;
    }

    void CheckIfShooting()
    {
        if (shooting) {
            if (ActiveBuffs.Contains("Rapid")) {
                if (Time.time - lastFired > (FireRate / 2))
                    Shoot();
            } 
            else 
                if (Time.time - lastFired > FireRate)
                    Shoot();
        }

        // Laser
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time-lastClickTime < catchTime && !transform.Find("Laser(Clone)"))
            {
                if (upgrades["Laser"] > 0)
                {
                    Instantiate(laser, transform);
                    shooting = false;
                    StartCoroutine(removeBuff("Laser", 5));
                    upgrades["Laser"] -= 1;
                }
                else
                    UIControl.instance.UIMessage("Not enough lasers");
                
            } else {
                //normal click
                
            }
            lastClickTime=Time.time;
        }
    }

    void Shoot()
    {
        Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 0.6f, 0), transform.rotation);

        if (ActiveBuffs.Contains("Triple")) 
        {
            Instantiate(bullet, new Vector3(transform.position.x + 0.2f, transform.position.y + 0.6f), Quaternion.Euler(0, 0, -5));
            Instantiate(bullet, new Vector3(transform.position.x - 0.2f, transform.position.y + 0.6f), Quaternion.Euler(0, 0, 5));
        }

        lastFired = Time.time;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Pickup")
        {
            string buffName = c.name.Replace("(Clone)", "");
            int buffTime = c.GetComponent<Pickup>().Time;
            ActiveBuffs.Add(buffName);
            StartCoroutine(removeBuff(buffName, buffTime));
                
            if (buffName == "Shield" && !transform.Find("Shield(Clone)"))
                Instantiate(shield, transform); 
        }

        if (c.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    void ClearBuffs()
    {
        ActiveBuffs.Clear();
    }

    void OnDestroy()
    {
        ClearBuffs();
        if (Death!= null)
            Death();
    }

    IEnumerator removeBuff (string buff, int time) {

        // Wait x seconds then remove the buff
        UIControl.instance.PickupTimer(buff, time);

        yield return new WaitForSeconds(time);
        ActiveBuffs.Remove(buff);

        if (buff == "Laser" || buff == "Shield") {
            Destroy (transform.Find(buff + "(Clone)").gameObject);
            shooting = true;
        }
    }
}