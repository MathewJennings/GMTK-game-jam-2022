using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    [SerializeField]
    float knockBackForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.CompareTag("Enemy"))
        {
            GetComponent<PlayerController>().TakeDamage();
            GetKnockedBack(collidedWith);
        }
    }

    private void GetKnockedBack(GameObject enemyThatKnockedBack)
    {
        Vector3 knockbackDirection = (transform.position - enemyThatKnockedBack.transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockBackForce);
    }
}