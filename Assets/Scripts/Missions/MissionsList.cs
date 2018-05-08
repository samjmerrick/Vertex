using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsList : MonoBehaviour
{
    public GameObject MissionPrefab;

    private void OnEnable()
    {
        ShowMissions();
    }

    private void OnDisable()
    {
        DeleteChildren();
    }

    public void ShowMissions()
    {
        DeleteChildren();

        foreach (Mission mission in Mission.GetMissions())
        {
            GameObject m = Instantiate(MissionPrefab, transform);
            m.GetComponent<UIMissionPrefab>().mission = mission;
        }
    }

    void DeleteChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
