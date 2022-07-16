using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    [SerializeField]
    float knockBackForce;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            GetComponent<PlayerController>().TakeDamage();
            GetKnockedBack(other.gameObject);
        }
    }

    private void GetKnockedBack(GameObject enemyThatKnockedBack)
    {
        Vector3 knockbackDirection = (transform.position - enemyThatKnockedBack.transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockBackForce);
    }
}