using UnityEngine;

public class ShipMove : MonoBehaviour
{
    // public
    public int speed;
  
    private Vector3 target;
    private Vector3 moveStartInput;
    private Vector3 moveStartPos;

    private bool moving;

    private Vector2 velocity = Vector2.zero;
    private Vector3 bounds;
    private Camera gamecamera;

    void Start()
    {
        bounds = Bounds.Get();

        target = transform.position;
        gamecamera = FindObjectOfType<Camera>();

    }

    void Update()
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
}