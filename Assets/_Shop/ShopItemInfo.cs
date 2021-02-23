using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemInfo : MonoBehaviour
{
    public ShopItemType Type;
    public ShopItem Item;
    public Text ItemDescription;
    public Slider LevelSlider;
    public ShopPurchaseButton PurchaseButton;
    
    public Text ShopDescription;

    private int upgradeLevel;

    void Awake()
    {
        PurchaseButton.Type = Type;
        PurchaseButton.Item = Item;

        UpdateItem();
    }

    public void UpdateItem()
    {
        upgradeLevel = Upgrades.Get(Item.ToString());

        if(Type == ShopItemType.Onetime) 
        {
            ItemDescription.text = Item.ToString() + " (" + upgradeLevel + ")";
        }
        else if(Type == ShopItemType.Upgrade)
        {
            ItemDescription.text = Item.ToString();
            LevelSlider.value = upgradeLevel;
        }
    }

    public void UpdateShopDescriptionToThis()
    {
        if(Type == ShopItemType.Onetime) 
        {
            ShopDescription.text = "Buy one " + Item.ToString();
        }
        else if (Type == ShopItemType.Upgrade) 
        {
            ShopDescription.text = "Increase the " + Item.ToString() + " duration";
        }
    }
}

public enum ShopItemType {Upgrade, Onetime};
public enum ShopItem {Laser, Rapid, Triple, Big, Shield};