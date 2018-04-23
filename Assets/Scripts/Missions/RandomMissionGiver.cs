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

    void Start()
    {
        while (Mission.GetMissions().Count < 3)
            StartMission(RandomMission());
    }

    public Mission RandomMission()
    {
        if (Mission.GetMissions().Count <= 2)
        {
            Mission newMission = null;

            while (newMission == null)
            {
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

                // If we already have a mission for this objective, loop back and create another
                if (MissionExists(newMission.NameOfObject))
                {
                    newMission = null;
                }
            }

            // Chance to be a per-game mission
            if (Random.Range(0, 2) == 0)
            {
                newMission.perGame = true;
                newMission.objective += " in one game";
                newMission.reward *= 2;
            }

            return newMission;
        }

        throw new System.Exception("There are 3 or more missions already in the MissionList!");
    }


    void StartMission(Mission mission)
    {
        Mission.missionList.Add(mission);
        mission.StartListener();
    }

    void StartMission(Mission mission, int i)
    {
        Mission.missionList.Insert(i, mission);
    }

    void StopMission(Mission mission)
    {
        Mission.missionList.Remove(mission);
        mission.StopListener();
    }

    public void SkipMission (Mission mission)
    {
        int originalPos = Mission.missionList.IndexOf(mission);
        StopMission(mission);

        StartMission(RandomMission(), originalPos);
    }

    void EndGame()
    {
        //MissionsList missionsList = FindObjectOfType<MissionsList>();

        List<Mission> clearedMissions = Mission.ClearMissions();
        foreach (Mission mission in clearedMissions)
            Debug.Log(mission.GetObjective());

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
}
