using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanel : MonoBehaviour {

    public GameObject missionList;
    public GameObject next;

    public int timeToShowMissionList = 5;

    IEnumerator Start()
    {
        next.SetActive(false);
        GameObject ml = Instantiate(missionList, transform);

        yield return new WaitForSeconds(timeToShowMissionList);
        Destroy(ml);
        next.SetActive(true);
    }
}
