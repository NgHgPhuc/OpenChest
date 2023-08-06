using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfomationFighting : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Cooldown;
    public TextMeshProUGUI Description;

    public static InfomationFighting Instance { get; private set; }
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

    public void Initialize(Transform button,BaseSkill skill)
    {
        Name.SetText(skill.Name);
        Cooldown.SetText("CD: " + skill.Cooldown + " Turns");
        Description.SetText(skill.Description);
    }
}
