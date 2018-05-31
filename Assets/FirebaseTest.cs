using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseTest : MonoBehaviour {

    private void Awake()
    {
        Firebase.Analytics.FirebaseAnalytics
          .LogEvent(
            Firebase.Analytics.FirebaseAnalytics.EventJoinGroup,
            Firebase.Analytics.FirebaseAnalytics.ParameterGroupId,
            "Hello World"
          );
    }
}
