using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //todo change this to weapon or something that
        if (other.CompareTag("Player"))
        {
            // GetComponent<EnemyHealth>().TakeDamage(other.transform, 1);
        }
    }
}
