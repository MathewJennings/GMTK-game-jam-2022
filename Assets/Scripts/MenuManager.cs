using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject victoryMenu;
    public GameObject defeatMenu;

    private void Awake()
    {
        GameManager.OnGameStateChange += OpenMenu;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= OpenMenu;
    }

    private void OpenMenu(GameState gs)
    {
        defeatMenu.SetActive(gs == GameState.Defeat);
        victoryMenu.SetActive(gs == GameState.RoomVictory);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
