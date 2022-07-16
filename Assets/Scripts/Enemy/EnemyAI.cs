using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    public float movementSpeed;
    [SerializeField]
    EnemyState currentState;

    private Rigidbody2D rigidBody;
    private GameObject player;

    private void Awake () {
        rigidBody = GetComponent<Rigidbody2D> ();
        player = GameObject.FindWithTag ("Player");
    }

    public void SetState(EnemyState newEnemyState)
    {
        currentState = newEnemyState;
    }

    private void FixedUpdate () {
        switch (currentState)
        {
            case EnemyState.Idling:
                break;
            case EnemyState.Patroling:
                break;
            case EnemyState.Chasing:
                chasePlayer();
                break;
        }
    }

    private void chasePlayer()
    {
        Vector2 heading = player.transform.position - transform.position;
        float distance = heading.magnitude;
        Vector2 direction = heading / distance;
        rigidBody.velocity = direction * movementSpeed;
    }
}

public enum EnemyState
{
    Idling,
    Patroling,
    Chasing
}