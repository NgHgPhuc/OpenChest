using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpenChestPanel : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth = 60;

    public Slider HealthBar;

    public Button LevelUpButton;
    public GameObject UpgradeChestPanel;
    void Start()
    {
        CurrentHealth = MaxHealth;
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = CurrentHealth;

        LevelUpButton.onClick.AddListener(LevelUpFunc);
    }

    public void ClickOnChest()
    {
        CurrentHealth -= 10;
        if (CurrentHealth > 0)
            HealthBar.value = CurrentHealth;
        else EarnChest();

    }
    public void EarnChest()
    {
        CurrentHealth = MaxHealth;
        HealthBar.value = CurrentHealth;
        ResourceManager.Instance.ChangeGold(20);
        ChestHandlerManager.Instance.RandomEquipment();
    }

    void LevelUpFunc()
    {
        UpgradeChestPanel.SetActive(true);
    }
}
