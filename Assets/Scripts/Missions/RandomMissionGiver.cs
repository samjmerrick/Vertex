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
	
	public void RandomMission()
    {
        if (Mission.GetMissions().Count <= 2)
        {
            Mission newMission = null;

            switch (Random.Range(0, 3))
            {
                case 0:
                    newMission = new DistanceMission();
                    break;

                case 1:
                    newMission = new KillMission();
                    break;
 
                case 2:
                    newMission = new PickupMission();
                    break;
            }

            // Chance to be a per-game mission
            if (Random.Range(0,2) == 0)
            {
                newMission.perGame = true;
                newMission.objective += " in one game";
                newMission.reward *= 2;
            }

            // If we already have a mission for this objective, return and create another
            if (MissionExists(newMission.NameOfObject))
            {
                RandomMission();
                return;
            }

            Mission.missionList.Add(newMission);
            newMission.StartListener();
        }
    }

    bool MissionExists(string nameOfObject)
    {
        foreach (Mission missioninfo in Mission.GetMissions())
        {
            if (missioninfo.NameOfObject == nameOfObject)
            {
                return true;
            }
        }
        return false;
    }

    void EndGame()
    {
        //MissionsList missionsList = FindObjectOfType<MissionsList>();

        List<Mission> clearedMissions = Mission.ClearMissions();
        foreach (Mission mission in clearedMissions)
            Debug.Log(mission.GetObjective());

    }
}
