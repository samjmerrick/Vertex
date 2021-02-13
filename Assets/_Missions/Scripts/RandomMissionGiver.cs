using UnityEngine;

public class RandomMissionGiver : MonoBehaviour {

    void Start()
    {
        while (Missions.GetMissions().Count < 3)
            StartMission(RandomMission());
    }

    public Mission RandomMission()
    {
        if (Missions.GetMissions().Count <= 2)
        {
            Mission newMission = null;

            while (newMission == null)
            {
                switch (Random.Range(0, 4))
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
                    case 3:
                        newMission = new ShootMission();
                        break;
                }

                // If we already have a mission for this objective, loop back and create another
                if (MissionExists(newMission.NameOfObject))
                {
                    newMission = null;
                }
            }

            return newMission;
        }

        throw new System.Exception("There are 3 or more missions already in the MissionList!");
    }

    void StartMission(Mission mission)
    {
        Missions.missionList.Add(mission);
        mission.StartListener();
    }

    void StartMission(Mission mission, int i)
    {
        Missions.missionList.Insert(i, mission);
        mission.StartListener();
    }

    void StopMission(Mission mission)
    {
        Missions.missionList.Remove(mission);
        mission.StopListener();
    }

    public void ReplaceMission (Mission mission)
    {
        int originalPos = Missions.missionList.IndexOf(mission);
        StopMission(mission);

        StartMission(RandomMission(), originalPos);
    }

    bool MissionExists(string nameOfObject)
    {
        foreach (Mission missioninfo in Missions.GetMissions())
        {
            if (missioninfo.NameOfObject == nameOfObject)
            {
                return true;
            }
        }
        return false;
    }
}
