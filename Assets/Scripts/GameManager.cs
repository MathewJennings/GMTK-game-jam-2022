using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public static event System.Action<GameState> OnGameStateChange;


    private List<GameObject> enemiesInLevel;

    private void Awake()
    {
        Instance = this;
        enemiesInLevel = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    void Start()
    {
        UpdateGameState(GameState.InGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateGameState(GameState newState) 
    {
        state = newState;

        switch (newState)
        {
            case GameState.SetupGame:
                break;
            case GameState.InGame:
                break;
            case GameState.DiceMenu:
                break;
            case GameState.Defeat:
                break;
            case GameState.RoomVictory:
                break;
            case GameState.ProceedingToNextLevel:
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(newState), newState, null);

        }
        OnGameStateChange?.Invoke(newState);
    }

    public void logEnemyDeath(GameObject enemy)
    {
        enemiesInLevel.Remove(enemy);
        if (enemiesInLevel.Count == 0)
        {
            UpdateGameState(GameState.RoomVictory);
        }
    }

    public void restartGame()
    {
        UpdateGameState(GameState.SetupGame);
    }

    public void proceedToNextLevel()
    {
        UpdateGameState(GameState.ProceedingToNextLevel);
    }
}
public enum GameState
{
    SetupGame,
    InGame,
    DiceMenu,
    Defeat,
    RoomVictory,
    ProceedingToNextLevel

}
