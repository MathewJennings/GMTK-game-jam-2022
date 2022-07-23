using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPersistedState : MonoBehaviour
{
    public static PlayerPersistedState Instance;
    public Inventory playerInventory;
    public Inventory playerBackpack;
    public bool isFreeplayMode;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        isFreeplayMode = false;
        DontDestroyOnLoad(gameObject);
    }

    public Inventory getPlayerInventory()
    {
        if (Instance == null)
        {
            Debug.Log("null playerinventory");
        }

        if (Instance.playerInventory == null)
        {
            Instance.playerInventory = new Inventory();
            Instance.playerInventory.AddItem(new Item { itemType = Item.ItemType.SwordDice });
        }
        return Instance.playerInventory;
    }
    public Inventory getPlayerBackpack()
    {
        if (Instance == null)
        {
            Debug.Log("null playerbackpack");
        }
        if (Instance.playerBackpack == null)
        {
            Instance.playerBackpack = new Inventory();
        }
        return Instance.playerBackpack;
    }
}
