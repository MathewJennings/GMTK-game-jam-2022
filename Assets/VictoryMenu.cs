using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour
{
    public GameObject backPackInventoryMenu;
    public UIInventory backPackInventoryUIInventory;
    public UIInventory rewardInventoryUIInventory;
    public UIInventory playerInventoryUIInventory;
    public TextMeshProUGUI title;
    public TextMeshProUGUI subtitle;
    private Button confirmButton;
    public bool isLastLevel;

    void Awake()
    {
        backPackInventoryMenu.SetActive(PlayerPersistedState.Instance.isFreeplayMode);
        confirmButton = GetComponentInChildren<Button>();
        CheckConfirmButton(null, null);
    }

    private void OnEnable()
    {
        backPackInventoryUIInventory.GetComponentInChildren<UIInventory>().inventory.OnItemListChanged += CheckConfirmButton;
        rewardInventoryUIInventory.GetComponentInChildren<UIInventory>().inventory.OnItemListChanged += CheckConfirmButton;
        playerInventoryUIInventory.GetComponentInChildren<UIInventory>().inventory.OnItemListChanged += CheckConfirmButton;

        bool isRewardsEmpty = rewardInventoryUIInventory.inventory.getCount() == 0;
        if(isLastLevel)
        {
            title.text = "You beat the game! Congratulations, You're dice-tacular!";
            subtitle.text = "Feel free to continue playing with your current die";
            PlayerPersistedState.Instance.isFreeplayMode = true;
        }
        if (isRewardsEmpty)
        {
            subtitle.text = "No rewards this time. Keep on rolling on.";
        }
    }

    private void OnDisable()
    {
        backPackInventoryUIInventory.GetComponentInChildren<UIInventory>().inventory.OnItemListChanged -= CheckConfirmButton;
        rewardInventoryUIInventory.GetComponentInChildren<UIInventory>().inventory.OnItemListChanged -= CheckConfirmButton;
        playerInventoryUIInventory.GetComponentInChildren<UIInventory>().inventory.OnItemListChanged -= CheckConfirmButton;
    }
    void CheckConfirmButton(object sender, System.EventArgs e)
    {
        if (!PlayerPersistedState.Instance.isFreeplayMode)
        {
            bool isRewardsEmpty = rewardInventoryUIInventory.inventory.getCount() == 0;
            confirmButton.interactable = isRewardsEmpty;
            confirmButton.GetComponentInChildren<TextMeshProUGUI>().text ="Go to Next Level";
        }
        if (isLastLevel)
        {
            confirmButton.GetComponentInChildren<TextMeshProUGUI>().text = "New Game +";

        }

    }
}
