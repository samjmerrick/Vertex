using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem : ShopItem {

    public Slider slider;
    public Text cost;

	protected override void SetUpgradeInfo(){

		if (upgradeLevel < 6) 
        {
            NextLevelDesc = "Increases the " + upgradeName + " duration";
            NextLevelCost = (upgradeLevel + 1) * ((upgradeLevel + 1) * 500);
        }

        else {
            NextLevelDesc = "All upgrades Completed";
            NextLevelCost = 0;
        }

        //Prepare the UI
        slider.value = upgradeLevel;
        slider.maxValue = 6;
        cost.text = NextLevelCost.ToString();

	}	
}
