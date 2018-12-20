using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanel : MonoBehaviour {

    public GameObject missionList;
    public GameObject next;

    public int timeToShowMissionList = 5;

    void Start()
    {
        // Count the total of ToComplete among missions, only show mission list if progress has been made
        int totalProgress = 0, totalCacheProgress = 0;

        foreach (Mission mission in Missions.GetMissions())
        {
            totalProgress += mission.progress;
            totalCacheProgress += mission.cacheProgress;
        }

        if (totalProgress != totalCacheProgress)
            StartCoroutine(ShowMissionList());             
    }

    IEnumerator ShowMissionList()
    {
        next.SetActive(false);
        GameObject ml = Instantiate(missionList, transform);

        yield return new WaitForSeconds(timeToShowMissionList);
        Destroy(ml);
        next.SetActive(true);
    }
}
