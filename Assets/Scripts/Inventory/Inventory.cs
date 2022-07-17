using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;

    public List<Item> getItems()
    {
        return itemList;
    }
    public int getCount()
    {
        return itemList.Count;
    }
    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    internal void SwapItem(Item item, Inventory other)
    {
        Debug.Log("Swapped");
        RemoveItem(item);
        other.AddItem(item);
    }
}
