using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class ResourceManager : MonoBehaviour
{
    float Gold = 500;
    public TextMeshProUGUI GoldMount;

    float Diamond = 500;
    public TextMeshProUGUI DiamondMount;

    float CurrentExp = 0f;
    float NeedExp = 500f;
    int PlayerLevel = 1;
    public TextMeshProUGUI Progress;
    public TextMeshProUGUI Level;


    public static ResourceManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        UpdateShowUI();


    }


    void UpdateShowUI()
    {
        GoldMount.SetText(Gold.ToString());
        DiamondMount.SetText(Diamond.ToString());

        Progress.SetText((CurrentExp * 100 / NeedExp) + "%");
        Level.SetText(PlayerLevel.ToString());
        ChestManager.Instance.PlayerLevel = PlayerLevel;
    }

    public void ChangeGold(float Mount)
    {
        Gold += Mount;
        UpdateShowUI();
    }

    public void GainExp(float value)
    {
        CurrentExp += value;
        float progress = CurrentExp * 100 / NeedExp;
        Progress.SetText(Math.Round(progress, 1) + "%");
        if (CurrentExp > NeedExp)
            LevelUp();
            
    }
    public void LevelUp()
    {
        PlayerLevel += 1;
        ChestManager.Instance.PlayerLevel = PlayerLevel;
        CurrentExp -= NeedExp;
        NeedExp += NeedExp * (10 + PlayerLevel)/ 100;

        float progress = CurrentExp * 100 / NeedExp;
        Progress.SetText(Math.Round(progress, 1) + "%");
        Level.SetText(PlayerLevel.ToString());

    }
}
