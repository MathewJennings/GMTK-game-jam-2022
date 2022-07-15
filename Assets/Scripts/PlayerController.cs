using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    [SerializeField] float speed;

    Vector2 movement;
    Rigidbody2D rigidBody;
    private int health;
    public int MAX_HEALTH = 5;
    public PlayerHealth playerHealth;

    private void Awake () {
        rigidBody = GetComponent<Rigidbody2D> ();
        SetHealth(MAX_HEALTH);
        playerHealth.numOfHearts = MAX_HEALTH;
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

    public void TakeDamage()
    {
        SetHealth(--health);
    }
    public void SetHealth(int h)
    {
        health = h;
        playerHealth.health = health;
        if(health == 0) {
            Die();
        }
    }
    public void Die()
    {
        speed = 0;
        GetComponent<Transform>().Rotate(0.0f, 0.0f, 90.0f, Space.Self);
    }
}