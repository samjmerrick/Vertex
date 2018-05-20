using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContinuePanel : MonoBehaviour {

    public Text Coins;
    public Text CountText;
    public int CountTime;

    private RectTransform rect;

    private void OnEnable()
    {
        Ship.Death += StartCountDown;
    }

    private void OnDisable()
    {
        Ship.Death -= StartCountDown;
    }

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void StartCountDown()
    {
        rect.anchoredPosition = new Vector2(0, 0);
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
        Time.timeScale = 1;
        StopAllCoroutines();
        rect.anchoredPosition = new Vector2(1000000000, 0);
    }
}
