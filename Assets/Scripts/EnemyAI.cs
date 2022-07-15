using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField] float speed;

    Rigidbody2D rigidBody;
    GameObject player;

    private void Awake () {
        rigidBody = GetComponent<Rigidbody2D> ();
        player = GameObject.FindWithTag ("Player");
    }

    private void FixedUpdate () {
        Vector2 heading = player.transform.position - transform.position;
        float distance = heading.magnitude;
        Vector2 direction = heading / distance;
        rigidBody.velocity = direction * speed;
    }
}