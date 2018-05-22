using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContinuePanel : MonoBehaviour {

    public GameObject ship;
    public Text Coins;
    public Text CountText;
    public int CountTime;

    private void OnEnable()
    {
        ShipTakeDamage.Death += StartCountDown;
    }

    private void OnDisable()
    {
        ShipTakeDamage.Death -= StartCountDown;
    }

    void StartCountDown()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(0.8f);

        GetComponentInParent<PanelManager>().ShowMenu(GetComponent<Panel>());

        Coins.text = PlayerPrefs.GetInt("Coins").ToString();
        Time.timeScale = 0.25f;

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
        ship.SetActive(true);
        Time.timeScale = 1;
        StopAllCoroutines();
    }

    public void GiveLifeForMoney(int value)
    {
        if (PlayerPrefs.GetInt("Coins") < value) return;

        PlayerPrefs.SetInt("Coins", (PlayerPrefs.GetInt("Coins") -value));
        UIControl.instance.Coins.text = PlayerPrefs.GetInt("Coins").ToString();
        GiveLife();
    }
}
