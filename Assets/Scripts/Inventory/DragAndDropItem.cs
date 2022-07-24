using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class DragAndDropItem : MonoBehaviour, 
    IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Item item;
    public Inventory originalInventory;
    private Vector3 initialRectTransformPosition;
    private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;
    private CanvasGroup canvasGroup;
    private CursorController cursorController;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        initialRectTransformPosition = rectTransform.anchoredPosition;
        cursorController = GameObject.FindObjectOfType<CursorController>();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        rectTransform.anchoredPosition = initialRectTransformPosition;
        cursorController.ChangeCursorToArrow();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
        rectTransform.position += Vector3.back;
        cursorController.ChangeCursorToClick();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }
    public void SetOriginalInventory(Inventory i)
    {
        this.originalInventory = i;
    }
}
