using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    float Gold = 500;
    float Diamond = 500;

    public TextMeshProUGUI GoldMount;
    public TextMeshProUGUI DiamondMount;

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
    }

    public void ChangeGold(float Mount)
    {
        Gold += Mount;
        UpdateShowUI();
    }
}
