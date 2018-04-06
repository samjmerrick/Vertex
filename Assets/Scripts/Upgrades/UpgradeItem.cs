using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem : ShopItem {

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
        transform.Find("Slider").GetComponent<Slider>().value = upgradeLevel;
        transform.Find("Slider").GetComponent<Slider>().maxValue = 6;
        transform.Find("Cost").GetComponent<Text>().text = NextLevelCost.ToString();

	}	
}
