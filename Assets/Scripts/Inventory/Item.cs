using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        SwordDice,
        SpinSwordDice
    }

    public ItemType itemType;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.SwordDice: return ItemAssets.Instance.swordDieSprite;
            case ItemType.SpinSwordDice: return ItemAssets.Instance.spinSwordDieSprite;
        }
    }
}
