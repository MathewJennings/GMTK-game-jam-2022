using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    [SerializeField] float initialSpeed;
    public Inventory playerInventory;
    public Inventory playerBackpack;
    float currentSpeed;
    Vector2 movement;
    Rigidbody2D rigidBody;
    private int health;
    public int MAX_HEALTH = 5;
    public PlayerHealth playerHealth;
    public PlayerAttackController playerAttackController;


    private void Awake () {
        rigidBody = GetComponent<Rigidbody2D> ();
        GameManager.OnGameStateChange += RestartTrigger;
        Restart();
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= RestartTrigger;
    }


    private void RestartTrigger(GameState gs)
    {
        if(gs == GameState.SetupGame)
        {
            Restart();
        }
    }
    private void Restart()
    {
        SetHealth(MAX_HEALTH);
        playerHealth.numOfHearts = MAX_HEALTH;
        currentSpeed = initialSpeed;
        GetComponent<Transform>().rotation = Quaternion.Euler(0.0f, 0.0f, 0);
        playerInventory = new Inventory();
        playerInventory.AddItem(new Item { itemType = Item.ItemType.SwordDice });
        playerBackpack = new Inventory();
    }

    private void FixedUpdate () {
        rigidBody.velocity = movement * currentSpeed;
    }

    public void OnMovement (InputAction.CallbackContext context) {
        movement = context.ReadValue<Vector2> ();
    }

    public void TakeDamage()
    {
        SetHealth(--health);
    }
    public void SetHealth(int h)
    {
        health = h;
        playerHealth.health = health;
        if(health <= 0) {
            Die();
        }
    }
    public void Die()
    {
        currentSpeed = 0;
        GetComponent<Transform>().Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        //await System.Threading.Tasks.Task.Delay(1000);
        GameManager.Instance.UpdateGameState(GameState.Defeat);
    }
}