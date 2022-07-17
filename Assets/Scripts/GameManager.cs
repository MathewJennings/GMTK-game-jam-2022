using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public static event System.Action<GameState> OnGameStateChange;
    public List<Transform> diceSpawnPoints;


    private List<GameObject> spawnedDice;
    private List<GameObject> enemiesInLevel;


    void Start()
    {
        Instance = this;
        enemiesInLevel = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        List<Item> inventoryDice = PlayerPersistedState.Instance.getPlayerInventory().getItems();
        spawnedDice = new List<GameObject>(inventoryDice.Count);
        for (int i = 0; i < inventoryDice.Count; i++)
        {
            var inventoryDie = inventoryDice[i];
            Object diePrefab = Resources.Load(inventoryDie.GetPrefabPath());
            var currentSpawnPoint = diceSpawnPoints[i % diceSpawnPoints.Count];
            //always spawn at z = -1;
            Vector3 dieInitialSpawnPoint = new Vector3(currentSpawnPoint.position.x, currentSpawnPoint.position.y, -1);
            spawnedDice.Add(Instantiate(diePrefab, dieInitialSpawnPoint, Quaternion.identity, currentSpawnPoint) as GameObject);
        }
        UpdateGameState(GameState.InGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateGameState(GameState newState) 
    {
        Debug.Log("Gamestate: " + newState);
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
        int livingEnemiesInLevel = 0;
        foreach(GameObject currentEnemy in enemiesInLevel)
        {
            if (currentEnemy.activeSelf)
            {
                livingEnemiesInLevel++;
            }
        }
        if (livingEnemiesInLevel == 0)
        {
            UpdateGameState(GameState.RoomVictory);
        }
    }

    public void restartGame()
    {
        for (int i = 0; i < diceSpawnPoints.Count; i++)
        {
            spawnedDice[i].transform.position = diceSpawnPoints[i].position;
            spawnedDice[i].transform.rotation= diceSpawnPoints[i].rotation;
        }
        foreach (GameObject enemy in enemiesInLevel)
        {
            enemy.SetActive(true);
        }
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
