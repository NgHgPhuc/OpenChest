using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class ResourceManager : MonoBehaviour
{
    float Gold = 500;
    public TextMeshProUGUI GoldMount;

    float Diamond = 50000;
    public TextMeshProUGUI DiamondMount;

    float CurrentExp = 0f;
    float NeedExp = 500f;
    int PlayerLevel = 1;
    public TextMeshProUGUI Progress;
    public TextMeshProUGUI Level;

    public FloatingObject floatingObject;

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

        FloatingObject f = Instantiate(floatingObject, GoldMount.transform.position, GoldMount.transform.rotation, GoldMount.transform);
        if(Mount < 0)
            f.Iniatialize(Mount.ToString(), Color.red,"Floating Down");
        else
            f.Iniatialize("+"+Mount, Color.green, "Floating Up");

        UpdateShowUI();
    }

    public void ChangeDiamond(float Mount)
    {
        Diamond += Mount;

        FloatingObject f = Instantiate(floatingObject, DiamondMount.transform.position, DiamondMount.transform.rotation, DiamondMount.transform);
        if (Mount < 0)
            f.Iniatialize(Mount.ToString(), Color.red, "Floating Down");
        else
            f.Iniatialize("+" + Mount, Color.green, "Floating Up");

        UpdateShowUI();
    }

    public void GainExp(float value)
    {
        CurrentExp += value;
        float progress = CurrentExp * 100 / NeedExp;

        FloatingObject f = Instantiate(floatingObject, Progress.transform.position, Progress.transform.rotation, Progress.transform);
        if (value < 0)
            f.Iniatialize(value.ToString(), Color.red, "Floating Down");
        else
            f.Iniatialize("+" + value, Color.green, "Floating Up");

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

    public bool CheckEnought_Gold(float value)
    {
        return (Gold > value);
    }

    public bool CheckEnough_Diamond(float value)
    {
        return (Diamond > value);
    }
}
