using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuffs : MonoBehaviour
{
    public GameObject shield;
    [HideInInspector]
    public List<string> Active = new List<string>();

    private void OnEnable()
    {
        ClearBuffs();
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Pickup")
        {
            Pickup pickup = c.GetComponent<Pickup>();
            StartCoroutine(removeBuff(pickup.Name, pickup.Time));
        }
    }

    public IEnumerator removeBuff(string buff, int time)
    {
        Active.Add(buff);

        if (buff == "Shield" && !transform.Find("Shield(Clone)"))
            Instantiate(shield, transform);

        // Wait x seconds then remove the buff
        UIControl.instance.PickupTimer(buff, time);
        yield return new WaitForSeconds(time);

        Active.Remove(buff);

        if (buff == "Laser" || buff == "Shield")
        {
            Destroy(transform.Find(buff + "(Clone)").gameObject);
            //shooting = true;
        }
    }

    void ClearBuffs()
    {
        Active.Clear();

        foreach (Transform child in transform)
        {
            if (child.name.Contains("Shield") || child.name.Contains("Laser"))
                Destroy(child.gameObject);
        }
            
    }
}

