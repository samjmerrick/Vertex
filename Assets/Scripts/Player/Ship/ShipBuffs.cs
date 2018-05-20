using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuffs : MonoBehaviour
{
    public GameObject shield;
    [HideInInspector]
    public List<string> Active = new List<string>();

    private void Start()
    {
        ClearBuffs();
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Pickup")
        {
            string buffName = c.name.Replace("(Clone)", "");
            int buffTime = c.GetComponent<Pickup>().Time;
            Active.Add(buffName);
            StartCoroutine(removeBuff(buffName, buffTime));

            if (buffName == "Shield" && !transform.Find("Shield(Clone)"))
                Instantiate(shield, transform);
        }
    }

    public IEnumerator removeBuff(string buff, int time)
    {
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
    }
}

