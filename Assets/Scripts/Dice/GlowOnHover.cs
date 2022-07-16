using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlowOnHover : MonoBehaviour {

    [SerializeField]
    Color hoverColor;

    private Camera mainCamera;
    private Renderer renderer;
    private Color originalColor;

    private void Awake () {
        mainCamera = Camera.main;
        renderer = GetComponentInChildren<Renderer> ();
        originalColor = renderer.material.color;
    }

    private void OnEnable () {
        DiceDragManager.diceStartHover += startDiceHover;
        DiceDragManager.diceEndHover += endDiceHover;
    }

    private void OnDisable () {
        DiceDragManager.diceStartHover -= startDiceHover;
        DiceDragManager.diceEndHover -= endDiceHover;
    }

    private void startDiceHover (GameObject diceBeingHovered) {
        if (diceBeingHovered == this.gameObject) {
            renderer.material.color = hoverColor;
        }
    }

    private void endDiceHover (GameObject diceBeingHovered) {
        if (diceBeingHovered == this.gameObject) {
            renderer.material.color = originalColor;
        }
    }
}