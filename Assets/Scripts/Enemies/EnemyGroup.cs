using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public Vector2 GroupSize;
    public float spacing;

    private Vector2 LeaderPosition;

    private IEnumerator Start()
    {
        enabled = false; // Disable this component so that new instantiations do not also create new objects

        int groupSize = (int)Random.Range(GroupSize.x, GroupSize.y + 1);

        LeaderPosition = transform.position;

        for (int i = 0; i < groupSize; i++)
        {        
            GameObject go = Instantiate(this.gameObject, LeaderPosition, transform.rotation);

            if (GetComponent<EnemyMoveOnPath>() != null)
                go.GetComponent<EnemyMoveOnPath>().leader = this.gameObject;

            yield return new WaitForSeconds(spacing);
        }
    }
}
