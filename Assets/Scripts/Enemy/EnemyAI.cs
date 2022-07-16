using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    public EnemyState initialState;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    GameObject initialSpawnPoint;

    private EnemyState currentState;
    private Rigidbody2D rigidBody;
    private GameObject player;

    private void Awake () {
        currentState = initialState;
        rigidBody = GetComponent<Rigidbody2D> ();
        player = GameObject.FindWithTag ("Player");
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
            transform.position = initialSpawnPoint.transform.position;
            currentState = initialState;
        }
    }

    public EnemyState GetEnemyState()
    {
        return currentState;
    }

    public void SetState(EnemyState newEnemyState)
    {
        currentState = newEnemyState;
    }

    private void FixedUpdate () {
        switch (currentState)
        {
            case EnemyState.Idling:
                returnToInitialSpawnPoint();
                break;
            case EnemyState.Patroling:
                break;
            case EnemyState.Chasing:
                chasePlayer();
                break;
        }
    }
    private void returnToInitialSpawnPoint()
    {
        Vector3 vectorToSpawnPoint = initialSpawnPoint.transform.position - transform.position;
        float distanceToSpawnPoint = vectorToSpawnPoint.magnitude;
        if (distanceToSpawnPoint > 0.01f)
        {
            Vector2 direction = vectorToSpawnPoint / distanceToSpawnPoint;
            rigidBody.velocity = direction * movementSpeed;
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
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