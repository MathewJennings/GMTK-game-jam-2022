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
    private Button confirmButton;
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

    }
}
