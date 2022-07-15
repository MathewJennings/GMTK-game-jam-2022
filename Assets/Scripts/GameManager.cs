using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public static event System.Action<GameState> OnGameStateChange;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
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
            default:
                throw new System.ArgumentOutOfRangeException(nameof(newState), newState, null);

        }
        OnGameStateChange?.Invoke(newState);
    }

    public void restartGame()
    {
        UpdateGameState(GameState.SetupGame);
    }
}
public enum GameState
{
    SetupGame,
    InGame,
    DiceMenu,
    Defeat,
    RoomVictory

}
