using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{

    public EnemyHealth enemyHealthBar;
    public int enemyMaxHealth;
    private int health;
    public EnemyAI enemyAI;

    //todo move this to an enemy manager or something. Doesn't respawn enemies right now
    private void Awake()
    {
        GameManager.OnGameStateChange += RestartTrigger;
    }
    private void RestartTrigger(GameState gs)
    {
        if (gs == GameState.SetupGame)
        {
            Restart();
        }
    }
    private void Restart()
    {
        enemyHealthBar.SetMaxHealth(enemyMaxHealth);
        SetHealth(enemyMaxHealth);
        enemyAI.SetSpeed(enemyAI.initialSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage()
    {
        Debug.Log("Take Damage");
        SetHealth(--health);
    }
    public void SetHealth(int h)
    {
        health = h;
        enemyHealthBar.SetHealth(health);
        if(health <= 0)
        {
            Die();
        } 
    }

    public void Die()
    {
        enemyAI.SetSpeed(0);
        Destroy(this.gameObject, 1);
        //TODO this should eventually be put on an enemy manager to check if all enemies are defeated
        GameManager.Instance.UpdateGameState(GameState.RoomVictory);
    }
}
