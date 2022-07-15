using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField] public float initialSpeed;
    private float currentSpeed;
    Rigidbody2D rigidBody;
    GameObject player;

    private void Awake () {
        rigidBody = GetComponent<Rigidbody2D> ();
        player = GameObject.FindWithTag ("Player");
        currentSpeed = initialSpeed;
    }

    private void FixedUpdate () {
        Vector2 heading = player.transform.position - transform.position;
        float distance = heading.magnitude;
        Vector2 direction = heading / distance;
        rigidBody.velocity = direction * currentSpeed;
    }

    public void SetSpeed(float s)
    {
        currentSpeed = s;
    }
}