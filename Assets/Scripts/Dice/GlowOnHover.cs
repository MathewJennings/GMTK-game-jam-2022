using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlowOnHover : MonoBehaviour {

    [SerializeField]
    Color hoverColor;

    private SpriteRenderer[] renderers;
    private Color[] originalColors;

    private void Awake () {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        originalColors = new Color[renderers.Length];
        Debug.Log(renderers);
        for (int i = 0; i < renderers.Length; i++) {
            Debug.Log(renderers[i]);
            originalColors[i] = renderers[i].color;
        }
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
            foreach(SpriteRenderer renderer in renderers) {
                renderer.color = hoverColor;
            }
        }
    }

    private void endDiceHover (GameObject diceBeingHovered) {
        if (diceBeingHovered == this.gameObject) {
            for (int i = 0; i < renderers.Length; i++) {
                renderers[i].color = originalColors[i];
            }
        }
    }
}