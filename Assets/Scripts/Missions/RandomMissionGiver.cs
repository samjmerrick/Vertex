using UnityEngine;

public class RandomMissionGiver : MonoBehaviour {

    private void OnEnable()
    {
        foreach (Mission mission in Mission.missionList)
        {
            if (mission.expireTimeTicks < System.DateTime.Now.Ticks)
            {
                Mission.missionList.Remove(mission);
                if (Mission.missionList.Count < 3)
                    RandomMission();
            }
        }
    }

    void Start ()
    {
        while (Mission.GetMissions().Count < 3)
            RandomMission();
    }
	
	public static void RandomMission()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
            PickupMission.RandomPickupMission(3);

        if (rand == 1)
            KillMission.KillRandom(20);
    }
}
