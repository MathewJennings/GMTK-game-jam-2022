using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameStateListener : MonoBehaviour
{

    [SerializeField]
    List<GameObject> enableGameObjectsOnRoomVictory;

    [SerializeField]
    List<GameObject> disableGameObjectsOnRoomVictory;

    private void OnEnable()
    {
        GameManager.OnGameStateChange += respondToGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= respondToGameStateChange;
    }

    private void respondToGameStateChange(GameState newGameState)
    {
        switch(newGameState)
        {
            case GameState.ProceedingToNextLevel:
                enableAndDisableGameObjects();
                break;
        }
    }

    private void enableAndDisableGameObjects()
    {
        foreach(GameObject toEnable in enableGameObjectsOnRoomVictory)
        {
            toEnable.SetActive(true);
        }
        foreach (GameObject toDisable in disableGameObjectsOnRoomVictory)
        {
            toDisable.SetActive(false);
        }
    }
}
