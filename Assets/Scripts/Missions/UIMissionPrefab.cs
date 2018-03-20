using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionPrefab : MonoBehaviour {

    public Mission mission;
    private Text expireTime;

    void Start()
    {
        if (mission != null)
        {
            int remaining = mission.ReturnRemaining();
            int toComplete = mission.ReturntoComplete();

            // Update Objective
            Text objective = transform.Find("Objective").gameObject.GetComponent<Text>();
            objective.text = mission.GetObjective() + " (" + remaining + "/" + toComplete + ")";   

            // Update Remaining Time
            expireTime = transform.Find("ExpireTime").gameObject.GetComponent<Text>();


            // Update image
            Image image = transform.Find("MissionImage").gameObject.GetComponent<Image>();
            image.sprite = (Sprite)Resources.Load("Buff_NoGlow/" + mission.NameOfObject, typeof(Sprite));
        }
    }

    private void OnGUI()
    {
        if (mission != null)
        {
            TimeSpan expiresIn = new TimeSpan(mission.expireTimeTicks - System.DateTime.Now.Ticks);

            if (mission.expireTimeTicks < DateTime.Now.Ticks)
                expireTime.text = "Last Chance!!";

            else
                if (expiresIn.Days > 0)
                expireTime.text = "Expires: " + String.Format("{0:0}d {1:0}h {2:0}m", expiresIn.Days, expiresIn.Hours, expiresIn.Minutes);
            else if (expiresIn.Hours > 0)
                expireTime.text = "Expires: " + String.Format("{0:0}h {1:0}m", expiresIn.Hours, expiresIn.Minutes);
            else
                expireTime.text = "Expires: " + String.Format("{0:0}m", expiresIn.Minutes);

            if (mission.remaining >= mission.toComplete)
                expireTime.text = "Complete";
        }
    }

    public void ChangeMission()
    {
        Start();
    }
}
