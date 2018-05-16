using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    #region SINGLETON PATTERN
    public static UIControl instance = null;
    public static UIControl _instance;
    public static UIControl Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIControl>();

                if (_instance == null)
                {
                    Debug.Log("no instance");
                }
            }

            return _instance;
        }
    }
    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject uiMessage;
    public GameObject RadialSlider;

    public Messages messages;

    public Text Distance;
    public Text Destroyed;
    public Text Laser;
    public Text Coins;

    private void OnEnable()
    {
        GameController.GameBegin += BeginGame;
    }

    private void OnDisable()
    {
        GameController.GameBegin -= BeginGame;
    }

    public void BeginGame()
    {
        Distance.text = "0";
        Destroyed.text = "0";
        
        Coins.text = PlayerPrefs.GetInt("Coins").ToString();
      
        if (Ship.upgrades.ContainsKey("Laser"))
            Laser.text = Ship.upgrades["Laser"].ToString();

        StartCoroutine(CreateMissionText());
    }

    IEnumerator CreateMissionText()
    {
        List<Mission> missions = Missions.GetMissions();

        for (int i = 0; i < missions.Count; i++)
        {
            yield return new WaitForSeconds(0.2f);
            messages.NewMessage("Mission: " + missions[i].objective, new Color32(255, 127, 80, 1));  
        }
    }

    public void GameMessage(string message)
    {
        messages.NewMessage(message);
    }
    
    public void UIMessage(string message)
    {
        uiMessage.GetComponent<Text>().text = message;
        uiMessage.GetComponent<Animation>().Play("textAnimation");
    }

    public void PickupTimer (string buff, int secs)
    {
        GameObject go = Instantiate(RadialSlider, transform.Find("BuffTimers"));
        go.GetComponent<BuffRadialSlider>().time = secs;
        go.GetComponent<BuffRadialSlider>().buff = buff;
    }
}
