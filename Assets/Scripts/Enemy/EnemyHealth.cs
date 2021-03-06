using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public EnemyHealthBarDiscrete enemyHealthBarDiscrete;
//    public EnemyHealthBar enemyHealthBar;
    public int enemyMaxHealth;
    private int health;
    public EnemyAI enemyAI;

    //todo move this to an enemy manager or something. Doesn't respawn enemies right now
    private void Awake()
    {
        Restart();
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChange += OnGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnGameStateChange;
    }

    private void OnGameStateChange(GameState newGameState)
    {
        if (newGameState == GameState.SetupGame)
        {
            Restart();
        }
    }
    private void Restart()
    {
        enemyHealthBarDiscrete.numOfHearts = enemyMaxHealth;
        //enemyHealthBar.SetMaxHealth(enemyMaxHealth);
        SetHealth(enemyMaxHealth);
        enemyAI.SetState(enemyAI.initialState);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(Transform t, int damage)
    {
        //todo set invuln timeout
        health -= damage;
        SetHealth(health);
    }
    public void SetHealth(int h)
    {
        health = h;
        //enemyHealthBar.SetHealth(health);
        enemyHealthBarDiscrete.health = health;
        if (health <= 0)
        {
            Die();
        } 
    }

    public void Die()
    {
        enemyAI.SetState(EnemyState.Idling);
        this.gameObject.SetActive(false);
        GameManager.Instance.logEnemyDeath(this.gameObject);
    }
}
