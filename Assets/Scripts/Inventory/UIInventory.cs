using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    private Inventory otherInventory; // not sure what this should look like yet. its playerInventory or rewardInventory
    public Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public int numSlots; 

    private void Awake()
    {
    }
    public void SetInventory(Inventory inventory, Inventory otherInventory)
    {
        this.inventory = inventory;
        this.otherInventory = otherInventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }
    private void OnDisable()
    {
        if(inventory!=null)
        {
            inventory.OnItemListChanged -= Inventory_OnItemListChanged;
        }
    }

    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = transform.Find("itemSlotTemplate");

        //destroy old transforms
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate)
            {
                continue;
            }
            else
            {
                Destroy(child.gameObject);
            }

        }

        int x = 0;
        int y = 0;
        int numItemsInRow = 1;
        float itemSlotCellSize = 85f;
        float paddingTop = 100f;
        float padding = 10f;
        var itemList = inventory.GetItemList();
        for(int i = 0; i < numSlots; i++)
        {

            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            Item item;
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            if (i < itemList.Count)
            {
                item = itemList[i];
                itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
                {

                };
                itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
                {
                    //move to other inventory
                    inventory.SwapItem(item, otherInventory);

                }; 
                image.sprite = item.GetSprite();
                var dndItem = image.gameObject.GetComponent<DragAndDropItem>();
                dndItem.SetItem(item);
                dndItem.SetOriginalInventory(inventory);
            } else
            {
                Destroy(image);
            }

            itemSlotRectTransform.anchoredPosition = new Vector2(padding + x * itemSlotCellSize, -(paddingTop + y * itemSlotCellSize));

            x++;
            if (x > numItemsInRow - 1)
            {
                x = 0;
                y++;
            }

        }
    }
}
