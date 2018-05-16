﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopItem : MonoBehaviour {

    public string upgradeName;

    [HideInInspector]
    protected int upgradeLevel = 0;
    [HideInInspector]
    public string NextLevelDesc;
    [HideInInspector]
    public int NextLevelCost;

    private UIShopController shopController;

    protected abstract void SetUpgradeInfo();

    void OnEnable()
    {
        transform.Find("Header").GetComponent<Text>().text = upgradeName;
        transform.Find("Image").GetComponent<Image>().sprite = (Sprite)Resources.Load("Buff_NoGlow/" + upgradeName, typeof(Sprite));

        // Find the current level, and set our variables
        if (!Ship.upgrades.TryGetValue(upgradeName, out upgradeLevel)) upgradeLevel = 0;

        SetUpgradeInfo();

        shopController = GetComponentInParent<UIShopController>();
    }


    private void OnMouseDown()
    {
        if (shopController.active != this)
            shopController.ChangeActive(this);
    }

    public void UpgradeBuff()
    {
        int coins = PlayerPrefs.GetInt("Coins");

        if (NextLevelCost <= coins)
        {
            // Upgrade and set preferences
            upgradeLevel++;
            PlayerPrefs.SetInt("Coins", coins - NextLevelCost);
            Ship.upgrades[upgradeName] = upgradeLevel;

            SetUpgradeInfo();
        }
        else
            print("not enough coins");
    }

    public void SetActive(bool active) 
    {
        if (active)
            GetComponent<Image>().color += new Color32(0, 0, 0, 200);
        else
            GetComponent<Image>().color -= new Color32(0, 0, 0, 200);
    }
}
