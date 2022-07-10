using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed;

    Vector2 movement;
    Rigidbody2D rigidBody;
    Unlockable unlockable;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rigidBody.velocity = movement * speed;
    }

    public void OnMovement(InputAction.CallbackContext context) {
        movement = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context) {
        Debug.Log("interacted!");
        if (unlockable != null)
        {
            unlockable.ToggleUnlockMenu();
        }
    }

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Unlockable temp = collision.gameObject.GetComponent<Unlockable>();
    //    if(temp != null)
    //    {
    //        unlockable = temp;
    //    }
    //}

    public void SetUnlockable(Unlockable u)
    {
        unlockable = u;
    }
}
