using UnityEngine;
using System.Collections.Generic;

public class RandomMissionGiver : MonoBehaviour {

    private void OnEnable()
    {
        GameController.GameEnd += EndGame;
    }

    private void OnDisable()
    {
        GameController.GameEnd -= EndGame;
    }

    void Start ()
    {
        while (Mission.GetMissions().Count < 3)
            RandomMission();
    }
	
	public static void RandomMission()
    {
        if (Mission.GetMissions().Count <= 2)
        {
            int rand = Random.Range(0, 2);

            if (rand == 0)
                PickupMission.RandomPickupMission(3);

            if (rand == 1)
                KillMission.KillRandom(20);
        }
    }

    void EndGame()
    {
        MissionsList missionsList = FindObjectOfType<MissionsList>();

        List<Mission> clearedMissions = Mission.ClearMissions();
        foreach (Mission mission in clearedMissions)
            Debug.Log(mission.GetObjective());

    }
}
