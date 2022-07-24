using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    public Texture2D arrowCursor;
    public Texture2D clickCursor;

    private void Awake()
    {
        ChangeCursorToArrow(null);
    }
    private void OnEnable()
    {
        DiceDragManager.diceStartHover += ChangeCursorToClick;
        DiceDragManager.diceEndHover += ChangeCursorToArrow;
    }

    private void OnDisable()
    {
        DiceDragManager.diceStartHover -= ChangeCursorToClick;
        DiceDragManager.diceEndHover -= ChangeCursorToArrow;
    }

    private void ChangeCursorToArrow(GameObject unused)
    {
        ChangeCursorToArrow();
    }

    public void ChangeCursorToArrow()
    {
        Cursor.SetCursor(arrowCursor, Vector2.zero, CursorMode.Auto);
    }

    private void ChangeCursorToClick(GameObject unused)
    {
        ChangeCursorToClick();
    }

    public void ChangeCursorToClick()
    {
        Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
    }
}
