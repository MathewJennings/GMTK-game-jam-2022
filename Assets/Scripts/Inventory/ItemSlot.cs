using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var isFreeplayMode = PlayerPersistedState.Instance.isFreeplayMode;
        // dont allow players to drop reward items
        if (!isFreeplayMode && gameObject.tag == "rewardMenuItemSlot")
        {
            return;
        }
        if(eventData.pointerDrag != null)
        {
            DragAndDropItem dragAndDropItem = eventData.pointerDrag.GetComponent<DragAndDropItem>();
            var originalInventory = dragAndDropItem.originalInventory;
            var itemSlotInventory = GetComponentInParent<UIInventory>().inventory;
            if (isFreeplayMode || originalInventory != itemSlotInventory)
            {
                originalInventory.SwapItem(dragAndDropItem.item, itemSlotInventory);
            }
        }
    }
}
