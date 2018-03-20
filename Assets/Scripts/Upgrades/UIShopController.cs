using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopController : MonoBehaviour {

    public ShopItem active;
    private Text description;
    private Button upgradeButton;
    private Text cost;

    private void OnEnable()
    {
        description = transform.Find("Description").GetComponent<Text>();
        upgradeButton = transform.Find("Upgrade Button").GetComponent<Button>();
        cost = transform.Find("Upgrade Button").GetComponentInChildren<Text>();
        description.text = "Select an upgrade";
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
    }
}
