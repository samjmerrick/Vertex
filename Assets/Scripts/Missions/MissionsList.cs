using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsList : MonoBehaviour
{
    public GameObject MissionPrefab;
    private List<GameObject> activeMissions = new List<GameObject>();



    private void OnEnable()
    {
        foreach (Mission mission in Mission.GetMissions())
        {
            GameObject m = Instantiate(MissionPrefab, transform);
            m.GetComponent<UIMissionPrefab>().mission = mission;

            activeMissions.Add(m);
        }
    }

    private void OnDisable()
    {
        foreach (GameObject Mission in activeMissions)
        {
            Destroy(Mission.gameObject);
        }
    }
}
