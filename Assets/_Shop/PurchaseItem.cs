using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseItem : ShopItem {

	protected override void SetUpgradeInfo(){
		NextLevelDesc = "Buy one " + upgradeName;
		NextLevelCost = 500;
		Name.text = upgradeName + " (" + upgradeLevel + ")";
        transform.Find("Cost").GetComponent<Text>().text = NextLevelCost.ToString();
    }
}
