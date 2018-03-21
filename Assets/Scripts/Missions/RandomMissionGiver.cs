using UnityEngine;

public class RandomMissionGiver : MonoBehaviour {

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
}
