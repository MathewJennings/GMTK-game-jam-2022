using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        None,
        SwordDice,
        SpinSwordDice,
        TeleportDice
    }

    public ItemType itemType;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.SwordDice: return ItemAssets.Instance.swordDieSprite;
            case ItemType.SpinSwordDice: return ItemAssets.Instance.spinSwordDieSprite;
            case ItemType.TeleportDice: return ItemAssets.Instance.teleportDieSprite;
        }
    }
    public string GetPrefabPath()
    {
        switch (itemType)
        {
            default:
            case ItemType.SwordDice: return "Attack Dice";
            case ItemType.SpinSwordDice: return "Spin Dice";
            case ItemType.TeleportDice: return "Teleport Dice";
        }
    }
}
