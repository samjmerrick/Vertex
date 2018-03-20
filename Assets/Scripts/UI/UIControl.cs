using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour {

    public static UIControl instance = null;

    public Text bestText;
    public Text scoreText;
    public Text comboText;
    public Slider comboSlider;

    private int best;
    private int score;
    private float buffTime;
    private Transform GameMenu;

    public GameObject uiMessage;

    public GameObject missionText;

    public GameObject coinupdate;

    public GameObject RadialSlider;

    #region SINGLETON PATTERN
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
        // Prepare the HUD
        best = PlayerPrefs.GetInt("best");
        bestText.text = "BEST: " + best;

        scoreText.text = "" + 0;

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

    public void FinishMission(Mission mission)
    {
        GameObject go = Instantiate(missionText, GameMenu);
        go.GetComponent<Text>().text = "COMPLETED: " + mission.objective;
        Destroy(go, 5);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int score)
    {
        if (score > best)
        {
            bestText.text = "BEST: " + score;
            PlayerPrefs.SetInt("best", score);
        }

        scoreText.text = "" + score;
    }
    
    public void UIMessage(string message)
    {
        uiMessage.GetComponent<Text>().text = message;
        uiMessage.GetComponent<Animation>().Play("textAnimation");
    }

    public void PickupTimer (string buff, int secs)
    {
        GameObject[] active = GameObject.FindGameObjectsWithTag("UIRadialBuff");

        GameObject go = Instantiate(RadialSlider, transform);
        go.GetComponent<BuffRadialSlider>().time = secs;
        go.GetComponent<BuffRadialSlider>().buff = buff;
        go.transform.position += new Vector3(0, active.Length * -0.75f);
    }

    public void SetCombo(string message) {
        comboText.text = message;
	}

    public void SetCombo(float i) {

        if (i >= 10) {
            comboSlider.fillRect.GetComponent<Image>().color = new Color(0, 0, 255, 0.5F);
            comboSlider.value = i - 10;
            SetCombo("x4");
        }
        
        if (i >= 5 && i <= 10) {
            comboSlider.fillRect.GetComponent<Image>().color = new Color(0, 255, 0, 0.5F);
            comboSlider.value = i - 5;
            SetCombo("x2");
        }
        if (i >= 0 && i <= 5) {
            comboSlider.fillRect.GetComponent<Image>().color = new Color(255, 0, 0, 0.5F);
            comboSlider.value = i;
            SetCombo("");
        }
    }

    public void CoinUpdate(int coins)
    {
        coinupdate.transform.GetChild(0).GetComponent<Text>().text = coins + "";
        coinupdate.GetComponent<Animation>().Play("MoveRight");
    }
}
