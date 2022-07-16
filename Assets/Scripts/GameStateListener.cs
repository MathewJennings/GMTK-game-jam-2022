using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateListener : MonoBehaviour
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
        if (newGameState == GameState.RoomVictory)
        {
            enableAndDisableGameObjects();
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
