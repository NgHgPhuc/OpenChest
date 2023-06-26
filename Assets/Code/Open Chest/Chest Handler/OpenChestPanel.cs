using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OpenChestPanel : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;

    public TextMeshProUGUI index;

    public Slider HealthBar;

    public Button LevelUpButton;
    public GameObject UpgradeChestPanel;
    public FloatingObject floatingObjectPref;
    Transform floatingPoint;
    void Start()
    {
        CurrentHealth = MaxHealth;
        index.SetText(CurrentHealth + "/" + MaxHealth);

        HealthBar.maxValue = MaxHealth;
        HealthBar.value = CurrentHealth;
        floatingPoint = transform.Find("Floating Point");

        LevelUpButton.onClick.AddListener(LevelUpFunc);
    }

    public void ClickOnChest()
    {
        CurrentHealth -= 10;
        if (CurrentHealth > 0)
        {
            index.SetText(CurrentHealth + "/" + MaxHealth);
            HealthBar.value = CurrentHealth;
            FloatingObject foPref = Instantiate(floatingObjectPref, floatingPoint.position, floatingPoint.rotation, transform);
            foPref.Iniatialize("-10", Color.red);
        }
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
