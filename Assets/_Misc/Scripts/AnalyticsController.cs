using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;

public class AnalyticsController : MonoBehaviour
{
    void OnEnable()
    {
        GameController.GameBegin += LogGameBegin;
        GameController.GameEnd += LogGameEnd;
        Mission.MissionComplete += LogMissionComplete;
    }

    void OnDisable()
    {
        GameController.GameBegin -= LogGameBegin;
        GameController.GameEnd -= LogGameEnd;
        Mission.MissionComplete -= LogMissionComplete;
    }

    void LogGameBegin()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
    }

    void LogGameEnd()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd);
    }

    void LogMissionComplete(Mission mission)
    {
        FirebaseAnalytics.LogEvent("completed_mission");
    }

    public static void LoggedIn()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
    }

    public static void LogSpendCurrency()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventSpendVirtualCurrency);
    }
}
