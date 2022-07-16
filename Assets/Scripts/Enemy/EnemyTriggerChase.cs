using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerChase : MonoBehaviour
{

    private EnemyAI enemyAI;
    private EnemyState stateBeforeChase;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lazyLoadEnemyAI();
            stateBeforeChase = enemyAI.GetEnemyState();
            enemyAI.SetState(EnemyState.Chasing);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lazyLoadEnemyAI();
            enemyAI.SetState(stateBeforeChase);
        }
    }

    private void lazyLoadEnemyAI()
    {
        if (enemyAI == null)
        {
            enemyAI = gameObject.transform.parent.gameObject.GetComponent<EnemyAI>();
        }
    }
}
