using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            DragAndDropItem dragAndDropItem = eventData.pointerDrag.GetComponent<DragAndDropItem>();
            dragAndDropItem.originalInventory.SwapItem(
                dragAndDropItem.item,
                GetComponentInParent<UIInventory>().inventory
            );
        }
    }
}
