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
        Restart();
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
    public void TakeDamage(Collider2D c)
    {
        //todo set invuln timeout
        Debug.Log("Take Damage health: " + health);
        SetHealth(--health);
        PushAway(c);
        Debug.Log("after Take Damage health: " + health);
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

    private void PushAway(Collider2D c)
    {
        // Calculate relative position from source to player. TODO CHANGE THIS TO CONTACT POINT
        Vector3 dir = transform.position - c.transform.position;
        // And finally we add force in the direction of dir and multiply it by force. 
        // TODO MAKE PUSHBACK AMOUNT A CONSTANT
        GetComponent<Rigidbody2D>().AddForce(dir.normalized * 2500);
    }
}
