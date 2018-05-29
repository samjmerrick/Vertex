using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContinuePanel : MonoBehaviour {

    public GameObject ship;
    public Text CoinsText;
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

        CoinsText.text = Coins.Get().ToString();
        Time.timeScale = 0.25f;

        for (int i = CountTime; i > 0; i--)
        {
            CountText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1); 
        }

        StopCountdown();  
    }

    public void StopCountdown()
    {
        StopAllCoroutines();
        Time.timeScale = 1;
        FindObjectOfType<GameController>().EndGame();
    }

    public void GiveLife()
    {
        StopAllCoroutines();
        Time.timeScale = 1;
        GetComponentInParent<PanelManager>().ShowMenu(transform.parent.Find("Game Menu").GetComponent<Panel>());
        ship.GetComponent<ShipTakeDamage>().GiveLife();
    }

    public void GiveLifeForMoney(int value)
    {
        bool lifeGiven = Coins.Debit(value);

        if (lifeGiven)
        {
            UIControl.instance.CoinsText.text = Coins.Get().ToString();
            GiveLife();
        }
    }
}
