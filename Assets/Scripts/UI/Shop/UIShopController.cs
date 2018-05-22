using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopController : MonoBehaviour {

    [HideInInspector]
    public ShopItem active;
    public Text description;
    public Button upgradeButton;
    public Text cost;
    public Text coins;

    private void OnEnable()
    {
        description.text = "Select an upgrade";
        coins.text = PlayerPrefs.GetInt("Coins").ToString();
        active = null;
        upgradeButton.enabled = false;
    }

    private void OnDisable()
    {
        if (active != null)
            active.SetActive(false);
    }

    public void ChangeActive(ShopItem changed)
    {
        if (active != null)
            active.SetActive(false);
        
        active = changed;
        active.SetActive(true);

        description.text = active.NextLevelDesc;

        if (active.NextLevelCost != 0)
        {
            if (active.NextLevelCost > PlayerPrefs.GetInt("Coins"))
            {
                cost.text = "Not enough coins";
                upgradeButton.enabled = false;
                return;
            }

            cost.text = active.NextLevelCost.ToString();
            upgradeButton.enabled = true;
        }
            
        else
        {
            cost.text = "Fully Upgraded";
            upgradeButton.enabled = false;
        }      
    }

    public void UpgradeBuff()
    {
        active.UpgradeBuff();
        ChangeActive(active);
        coins.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
