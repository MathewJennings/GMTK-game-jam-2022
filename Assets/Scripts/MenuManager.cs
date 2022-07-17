using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public PlayerController PlayerController;
    public GameObject rewardInventory;
    public GameObject victoryMenu;
    public GameObject defeatMenu;
    [SerializeField] private UIInventory PlayerUIInventory;
    [SerializeField] private UIInventory backpackUIInventory;

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
        if(gs==GameState.RoomVictory)
        {
            PlayerUIInventory.SetInventory(PlayerController.playerInventory, PlayerController.playerBackpack);
            backpackUIInventory.SetInventory(PlayerController.playerBackpack, PlayerController.playerInventory);
            rewardInventory.GetComponent<UIInventory>().SetInventory(ItemAssets.Instance.rewardInventory, PlayerController.playerInventory);
        }
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
