using UnityEngine;
using UnityEngine.UI;

public class BuffRadialSlider: MonoBehaviour
{
    public string buff;

    private float angle;
    private Image image;

    private void OnEnable()
    {
        ShipTakeDamage.Death += Destroy;
    }

    private void OnDisable()
    {
        ShipTakeDamage.Death -= Destroy;
    }

    private void Start()
    {
        image = GetComponent<Image>();
        transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load("Buff_NoGlow/" + buff, typeof(Sprite));
    }

    public void UpdateAngle(float amount)
	{
        // Update angle
        angle = amount;

        // Fillamount + color
        if (image != null)
        {
            image.fillAmount = angle;
            image.color = Color.Lerp(Color.red, Color.green, angle);

        }

        // Check if should be destroyed
        if (angle <= 0 || !GameController.GameRunning) {
            Destroy();
        }
	}

    private void Destroy()
    {
        Destroy(gameObject);
    }
}


