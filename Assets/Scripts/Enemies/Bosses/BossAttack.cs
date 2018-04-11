using System.Collections;
using UnityEngine;

[System.Serializable]
public class BossAttack {

    public Transform[] ShootPoints;
    public GameObject Bullet;

    public float ShootFrequency;
    public int ShootSpeed;
    public int clipSize;

    private int numberShot;

    public IEnumerator Attack()
    {
        numberShot = 0;

        for (int i = 0; i < clipSize; i++)
        {
            foreach (Transform transform in ShootPoints)
            {
                GameObject go = Object.Instantiate(Bullet, transform.position, transform.rotation);
                go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * ShootSpeed);
            }

            numberShot++;

            yield return new WaitForSeconds(ShootFrequency);
        }     
    }

    public bool IsEmpty()
    {
        return numberShot == clipSize;
    }
}
