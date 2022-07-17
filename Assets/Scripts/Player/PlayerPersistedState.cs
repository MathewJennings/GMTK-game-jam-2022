using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPersistedState : MonoBehaviour
{
    public static PlayerPersistedState Instance;
    public Inventory playerInventory;
    public Inventory playerBackpack;
    private void Awake()
    {
        Debug.Log("awake");
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Inventory getPlayerInventory()
    {
        Debug.Log("getplayerinventory");
        if (Instance == null)
        {
            Debug.Log("null playerinventory");
        }

        if (Instance.playerInventory == null)
        {
            Debug.Log("creating new playerinventory");
            Instance.playerInventory = new Inventory();
            Instance.playerInventory.AddItem(new Item { itemType = Item.ItemType.SwordDice });
        }
        return Instance.playerInventory;
    }
    public Inventory getPlayerBackpack()
    {
        Debug.Log("getplayerbackpack");
        if (Instance == null)
        {
            Debug.Log("null playerbackpack");
        }
        if (Instance.playerBackpack == null)
        {
            Debug.Log("creating new backpack");
            Instance.playerBackpack = new Inventory();
        }
        return Instance.playerBackpack;
    }
}
