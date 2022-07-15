using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{

    public EnemyHealth enemyHealthBar;
    public int enemyMaxHealth;
    public int health;
    public EnemyAI enemyAI;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealthBar.SetMaxHealth(enemyMaxHealth);
        enemyHealthBar.SetHealth(health);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage()
    {
        SetHealth(--health);
    }
    public void SetHealth(int h)
    {
        health = h;
        enemyHealthBar.SetHealth(health);
        if(health == 0)
        {
            Die();
        } 
    }

    public void Die()
    {
        enemyAI.SetSpeed(0);
        Destroy(this.gameObject, 1);
    }
}
