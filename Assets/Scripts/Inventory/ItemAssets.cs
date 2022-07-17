using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    public Inventory rewardInventory;
    public Item.ItemType rewardItemType;

    private void Awake()
    {
        Instance = this;
        rewardInventory = new Inventory();
        if(rewardItemType != Item.ItemType.None)
        {
            rewardInventory.AddItem(new Item { itemType = rewardItemType });
        }
    }

    public Inventory getRewardInventory()
    {
        return rewardInventory;
    }

    public Sprite swordDieSprite;
    public Sprite spinSwordDieSprite;
    public Sprite teleportDieSprite;
}
