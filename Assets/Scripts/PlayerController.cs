using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed;

    Vector2 movement;
    Rigidbody2D rigidBody;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rigidBody.velocity = movement * speed;
    }

    private void OnMovement(InputValue value) {
        movement = value.Get<Vector2>();
    }

    private void OnInteract() {
        Debug.Log("interacted!");
    }
}
