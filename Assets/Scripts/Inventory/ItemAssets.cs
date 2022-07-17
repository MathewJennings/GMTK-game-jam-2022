using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    public Inventory rewardInventory;

    private void Awake()
    {
        Instance = this;
        rewardInventory = new Inventory();
        rewardInventory.AddItem(new Item { itemType = Item.ItemType.SpinSwordDice } );
    }

    public Sprite swordDieSprite;
    public Sprite spinSwordDieSprite;
}
