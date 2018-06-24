using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public int GroupSize;
    public float spacing;

    private void Start()
    {
        enabled = false; // Disable this component so that new instantiations do not also create new objects

        for (int i = 0; i < GroupSize; i++)
        {
            Vector2 pos = (transform.position - transform.forward * spacing) + new Vector3(0, i);
        
            Instantiate(this.gameObject, pos, transform.rotation);
        }
    }
}
