using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour
{
    // Movement Variables
    public int speed;
    private Vector3 target;
    private Vector3 moveStartInput;
    private Vector3 moveStartPos;

    private bool moving;

    private Vector2 velocity = Vector2.zero;
    private Vector3 bounds;
    private Camera gamecamera;
    public float exSpace = 0.5f;

    // Events
    public delegate void Died();
    public static event Died Death;

    private void OnEnable()
    {
        GameController.GameEnd += Destroy;
    }

    private void OnDisable()
    {
        GameController.GameEnd -= Destroy;
    }

    void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        bounds.x += exSpace;

        target = transform.position;
        gamecamera = FindObjectOfType<Camera>();

    }

    void Update()
    {
        Move();
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
        {
            moving = false;
        }
            
    }

   
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Enemy" || c.gameObject.tag == "EnemyFire")
        {
            if (Death != null)
                Death();
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}