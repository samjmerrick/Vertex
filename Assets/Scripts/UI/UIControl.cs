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
    public GameObject missionText;
    public GameObject RadialSlider;

    public Messages messages;

    public Text Distance;
    public Text Destroyed;
    public Text Laser;
    public Text Coins;

    private Transform GameMenu;

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

        GameMenu = transform.Find("Game Menu");

        StartCoroutine(CreateMissionText());
    }

    IEnumerator CreateMissionText()
    {
        int i = 0;

        foreach (Mission mission in Mission.GetMissions())
        {
            GameObject go = Instantiate(missionText, GameMenu);
            go.transform.position += new Vector3(0, -i * 0.6f);
            go.GetComponent<Text>().text = mission.objective;
            Destroy(go, 5);
            yield return new WaitForSeconds(0.4f);
            i++;
        }
    }

    public void GameMessage(string message)
    {
        messages.Message(message);
    }
    
    public void UIMessage(string message)
    {
        uiMessage.GetComponent<Text>().text = message;
        uiMessage.GetComponent<Animation>().Play("textAnimation");
    }

    public void PickupTimer (string buff, int secs)
    {
        GameObject go = Instantiate(RadialSlider, GameMenu.transform.Find("BuffTimers"));
        go.GetComponent<BuffRadialSlider>().time = secs;
        go.GetComponent<BuffRadialSlider>().buff = buff;
    }
}
