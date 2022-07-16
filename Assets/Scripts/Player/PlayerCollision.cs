using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Debug.Log ("Collided With Enemy!!");
            GetComponent<PlayerController>().TakeDamage();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Debug.Log ("Ran away from Enemy!!");
        }
    }
}