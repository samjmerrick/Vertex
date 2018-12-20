using System.Collections.Generic;

public static class Missions
{
    public static List<Mission> missionList = new List<Mission>();
    public static List<Mission> GetMissions() { return new List<Mission>(missionList); }

    public static void LoadMissions(List<Mission> missions)
    {
        foreach (Mission mission in missionList)
            mission.StopListener();

        missionList.Clear();
        missionList = missions;

        foreach (Mission mission in missionList)
        {
            mission.StartListener();
            mission.cacheProgress = mission.progress; // Cache the progress at the start of the game

            // If this is a Per-Game Mission, progress = 0
            if (mission.perGame)
            {
                mission.progress = 0;
            }
        }
    }

    public static List<Mission> ClearMissions()
    {
        List<Mission> clearedMissions = new List<Mission>();

        foreach (Mission mission in GetMissions())
        {
            if (mission.progress >= mission.toComplete)
            {
                missionList.Remove(mission);
                clearedMissions.Add(mission);
            }
        }

        return clearedMissions;
    }
}

