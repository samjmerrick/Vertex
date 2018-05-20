using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContinuePanel : MonoBehaviour {

    public Text Coins;
    public Text CountText;
    public int CountTime;

    private void OnEnable()
    {
        Ship.Death += StartCountDown;
    }

    private void OnDisable()
    {
        Ship.Death -= StartCountDown;
    }


    void StartCountDown()
    {
        GetComponentInParent<PanelManager>().ShowMenu(GetComponent<Panel>());
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        Coins.text = PlayerPrefs.GetInt("Coins").ToString();
        Time.timeScale = 0;

        for (int i = CountTime; i > 0; i--)
        {
            CountText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1); 
        }
 
        Time.timeScale = 1;
        FindObjectOfType<GameController>().EndGame();
    }

    public void GiveLife()
    {
        GetComponentInParent<PanelManager>().ShowMenu(transform.parent.Find("Game Menu").GetComponent<Panel>());
        Time.timeScale = 1;
        StopAllCoroutines();
    }
}
