using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float Speed = 4;
    public float RotSpeed = 200;
    public int Health;

    private Canvas healthBar;
    private Slider healthBarSlider;
    private Renderer rend;

    private bool collided = false;

    public delegate void EnemyDied(string name, Vector3 transform );
    public static event EnemyDied Death;

    private void Awake()
    {
        SpawnController.EnemiesRemaining++;
        rend = GetComponent<Renderer>();
        //Speed += (GameController.gameStats["Destroyed"] * 0.005f);
    }

    private void OnDestroy()
    {
        SpawnController.EnemiesRemaining--;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
            Destroy(gameObject);

        if (c.gameObject.tag == "PlayerFire" && GetComponent<Renderer>().isVisible)
            DecreaseHealth();
    }

    public void DecreaseHealth()
    {
        Health -= 1;

        if (!transform.Find("Health Bar(Clone)") && Health >= 2 && !name.Contains("_"))
        {
            healthBar = Instantiate((Canvas)Resources.Load("Enemies/Health Bar", typeof(Canvas)), transform);
            healthBarSlider = healthBar.GetComponentInChildren<Slider>();
            healthBarSlider.maxValue = Health + 1;
            healthBarSlider.value = Health;
        }

        StartCoroutine(Flash());

        if (healthBar)
            healthBarSlider.value = Health;

        if (Health <= 0 && !collided)
        {
            collided = true;

            if (Death != null)
                Death(gameObject.name, transform.position);

            Destroy(gameObject, 0.05f);
        }
    }

    IEnumerator Flash()
    {
        rend.material.SetFloat("_FlashAmount", 0.75f);
        yield return new WaitForSeconds(0.075f);
        rend.material.SetFloat("_FlashAmount", 0);
    }
}
