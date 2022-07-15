using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    [SerializeField] float speed;

    Vector2 movement;
    Rigidbody2D rigidBody;

    private void Awake () {
        rigidBody = GetComponent<Rigidbody2D> ();
    }

    private void FixedUpdate () {
        rigidBody.velocity = movement * speed;
    }

    public void OnMovement (InputAction.CallbackContext context) {
        movement = context.ReadValue<Vector2> ();
    }

    public void OnInteract (InputAction.CallbackContext context) {
        Debug.Log("Interacted!");
    }
}