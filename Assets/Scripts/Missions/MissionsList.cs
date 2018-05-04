using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsList : MonoBehaviour
{
    public GameObject MissionPrefab;
    private List<GameObject> activeMissions = new List<GameObject>();

    private void OnEnable()
    {
        PanelManager.ChangePanel += CheckIfActive;
    }

    private void OnDisable()
    {
        PanelManager.ChangePanel -= CheckIfActive;
    }

    void ShowMissions()
    {
        DeleteMissions();

        foreach (Mission mission in Mission.GetMissions())
        {
            GameObject m = Instantiate(MissionPrefab, transform);
            m.GetComponent<UIMissionPrefab>().mission = mission;

            activeMissions.Add(m);
        }
    }

    void DeleteMissions()
    {
        foreach (GameObject Mission in activeMissions)
        {
            Destroy(Mission.gameObject);
        }
    }

    void CheckIfActive(Panel panel)
    {
        if (transform.parent.parent.GetComponent<Panel>() == panel)
        {
            RefreshList();
        } 
    }

    public void RefreshList()
    {
        ShowMissions();
    }
}
