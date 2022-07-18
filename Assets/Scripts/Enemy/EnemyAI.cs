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

    [SerializeField]
    List<GameObject> patrolPoints;

    private EnemyState currentState;
    private Rigidbody2D rigidBody;
    private GameObject player;
    private int nextPatrolPointIndex;

    private void Awake () {
        rigidBody = GetComponent<Rigidbody2D> ();
        player = GameObject.FindWithTag ("Player");
        Reset();
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
            Reset();
        }
    }

    private void Reset()
    {

        transform.position = initialSpawnPoint.transform.position;
        currentState = initialState;
        nextPatrolPointIndex = 0;
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
            case EnemyState.HoldPosition:
                holdPosition();
                break;
            case EnemyState.Idling:
                returnToInitialSpawnPoint();
                break;
            case EnemyState.Patroling:
                moveToNextPatrolPoint();
                break;
            case EnemyState.Chasing:
                chasePlayer();
                break;
        }
    }

    private void holdPosition()
    {
        rigidBody.velocity = Vector2.zero;
    }
    private void returnToInitialSpawnPoint()
    {
        bool reachedPosition = moveToPosition(initialSpawnPoint.transform.position);
        if (reachedPosition) {
            rigidBody.velocity = Vector2.zero;
        }
    }
    private void moveToNextPatrolPoint()
    {
        bool reachedPosition = moveToPosition(patrolPoints[nextPatrolPointIndex].transform.position);
        if (reachedPosition)
        {
            nextPatrolPointIndex = (nextPatrolPointIndex == patrolPoints.Count - 1) ? 0 : nextPatrolPointIndex + 1;
        }
    }

    private void chasePlayer()
    {
        moveToPosition(player.transform.position);
    }

    private bool moveToPosition(Vector3 position)
    {
        Vector3 vectorToPosition = position - transform.position;
        float distanceToPosition = vectorToPosition.magnitude;
        if (distanceToPosition > 0.03f)
        {
            Vector2 direction = vectorToPosition / distanceToPosition;
            rigidBody.velocity = direction * movementSpeed;
            return false;
        }
        else
        {
            return true;
        }
    }
}

public enum EnemyState
{
    Idling,
    Patroling,
    Chasing,
    HoldPosition
}