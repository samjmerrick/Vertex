using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopPurchaseButton : MonoBehaviour
{
    public Text Cost;
    
    // These are set by ShopSetInfo
    [HideInInspector]
    public ShopItem Item;
    [HideInInspector]
    public ShopItemType Type;

    private int currentCoins;
    private int priceOfUpgrade;
    private int upgradeLevel;
    private Button button;

    void Start() 
    {
        button = GetComponent<Button>();
        CheckPriceOfUpgrade();
    }

    void CheckPriceOfUpgrade()
    {
        // Work out how much the item costs
        upgradeLevel = Upgrades.Get(Item.ToString());

        if(Type == ShopItemType.Upgrade) 
        {
            priceOfUpgrade = (upgradeLevel + 1) * ((upgradeLevel + 1) * 500);    
        }
        else if (Type == ShopItemType.Onetime)
        {
            priceOfUpgrade = 150;
        }

        Cost.text = priceOfUpgrade.ToString();

        // Check how much the 
        currentCoins = Coins.Get();
        
        if (currentCoins < priceOfUpgrade) 
        {
            button.interactable = false;
        }
        else 
        {
            button.interactable = true;
        }
    }

    public void AttemptPurchase()
    {
        if (priceOfUpgrade <= Coins.Get())
        {
            // Upgrade and set preferences
            upgradeLevel++;
            Coins.Debit(priceOfUpgrade);
            Upgrades.Amend(Item.ToString(), 1);

            CheckPriceOfUpgrade();
            GetComponentInParent<ShopItemInfo>().UpdateItem();
        }
    }
}

