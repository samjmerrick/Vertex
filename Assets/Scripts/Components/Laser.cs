using UnityEngine;

public class Laser : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            c.GetComponent<Enemy>().DecreaseHealth();
        }
    }
}
