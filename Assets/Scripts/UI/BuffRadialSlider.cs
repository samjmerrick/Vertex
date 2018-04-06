using UnityEngine;
using UnityEngine.UI;

public class BuffRadialSlider: MonoBehaviour
{
    public int time;
    public string buff;

    private float angle;
    private float timeRemaining;

    private void Start()
    {
        timeRemaining = Time.time + time;
        transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load("Buff_NoGlow/" + buff, typeof(Sprite));
    }

    void Update()
	{
        angle = (timeRemaining - Time.time) / time;
		GetComponent<Image>().fillAmount = angle;
		GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, angle);

        if (angle <= 0 || !GameController.instance.GameRunning) {
            Destroy(gameObject);

        }
	}
}


