using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMission : MonoBehaviour
{
    public GameObject MissionPrefab;
    private List<GameObject> activeMissions = new List<GameObject>();

    private void OnEnable()
    {
        List<Mission> missionList = Mission.GetMissions();

        for (int i = 0; i < missionList.Count; i++)
        {
            GameObject m =
                Instantiate(MissionPrefab,
                new Vector3(transform.position.x, transform.position.y + (-1.4f * i) + 1.5f),
                transform.rotation,
                transform);
            m.GetComponent<UIMissionPrefab>().mission = missionList[i];

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
