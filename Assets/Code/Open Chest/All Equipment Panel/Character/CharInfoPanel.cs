using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharInfoPanel : MonoBehaviour
{
    public GameObject Panel;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Quality;
    public TextMeshProUGUI Power;
    public Image CharacterIcon;

    public Transform starPanel;

    public Transform StatsPanel;

    public Animator animator;

    public static CharInfoPanel Instance { get; private set; }

    List<SlotStatsPanel> AllStatsPanel;
    List<SlotStatsPanel> AllPassivePanel;

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
        AllStatsPanel = new List<SlotStatsPanel>();
        AllPassivePanel = new List<SlotStatsPanel>();

        for (int i = 0; i < StatsPanel.childCount; i++)
        {
            if (i < 4)
                AllStatsPanel.Add(StatsPanel.GetChild(i).GetComponent<SlotStatsPanel>());
            else
                AllPassivePanel.Add(StatsPanel.GetChild(i).GetComponent<SlotStatsPanel>());

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowInfo(Character character)
    {
        Panel.SetActive(true);

        if (character == null)
            return;

        Name.SetText(character.Name);
        Quality.SetText(character.tier.ToString());
        Quality.color = character.GetColor();
        Power.SetText(character.PowerPoint().ToString());
        CharacterIcon.sprite = character.Icon;

        for (int i = 0; i < starPanel.childCount; i++)
            starPanel.GetChild(i).GetComponent<Image>().color = (i < character.currentStarCount) ? Color.white : Color.black;

        AllStatsPanel[0].SetStatsValue(character.AttackDamage);
        AllStatsPanel[1].SetStatsValue(character.HealthPoint);
        AllStatsPanel[2].SetStatsValue(character.DefensePoint);
        AllStatsPanel[3].SetStatsValue(character.Speed);

        foreach (KeyValuePair<Equipment.Passive, float> kvp in character.PassiveList)
            AllPassivePanel[(int)kvp.Key - 1].SetStatsValue(kvp.Value, 1);

        animator.Play("Open");
    }

    public void ClickBackground()
    {
        Panel.SetActive(false);

    }

}
