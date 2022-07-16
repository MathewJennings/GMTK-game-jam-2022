using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerNextLevel : MonoBehaviour
{
    [SerializeField]
    string nameOfLevelToTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Go to next level!");
            SceneManager.LoadScene(nameOfLevelToTrigger);
        }
    }
}
