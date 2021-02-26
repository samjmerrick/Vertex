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

    private int upgradeLevel;

    void Awake()
    {
        PurchaseButton.Type = Type;
        PurchaseButton.Item = Item;
    }

    void OnEnable()
    {
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
}

public enum ShopItemType {Upgrade, Onetime};
public enum ShopItem {Laser, Rapid, Triple, Big, Shield};