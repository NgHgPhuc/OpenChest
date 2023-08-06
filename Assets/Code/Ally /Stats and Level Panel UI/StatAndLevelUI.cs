using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.TextCore.Text;

public class StatAndLevelUI : MonoBehaviour
{
    [Header("Stats Panel")]
    public Transform statsPanel;
    List<SlotStatsPanel> AllStatsPanel = new List<SlotStatsPanel>();
    List<SlotStatsPanel> AllPassivePanel = new List<SlotStatsPanel>();

    [Header("Level Up Panel")]
    public Image AddExpSlider;
    float AddExpValue;

    float NeedExpSimulator;
    public Image CurrentExpSlider;
    float CurrentExpSimulator;

    public TextMeshProUGUI Level;
    int CurrentLevelSimulator;
    public TextMeshProUGUI AddExpText;
    public TextMeshProUGUI CurrentExpProgessText;

    [Header("Potion Panel")]
    public ItemSlot SmallPotion;
    public ItemSlot MediumPotion;
    public ItemSlot LargePotion;
    public ItemSlot SuperPotion;

    public void SetAllyUI(Character character)
    {
        if (character == null)
            return;


        Set_StatsPanel(character);
        Set_LevelUpPanel(character);
        Set_PotionPanel();
    }

    void Set_StatsPanel(Character character)
    {
        if (character == null)
            return;

        if (AllStatsPanel.Count == 0 || AllPassivePanel.Count == 0)
            for (int i = 0; i < statsPanel.childCount; i++)
                if (i < 4)
                    AllStatsPanel.Add(statsPanel.GetChild(i).GetComponent<SlotStatsPanel>());
                else
                    AllPassivePanel.Add(statsPanel.GetChild(i).GetComponent<SlotStatsPanel>());

        AllStatsPanel[0].SetValue_NotIncremental(character.AttackDamage);
        AllStatsPanel[1].SetValue_NotIncremental(character.HealthPoint);
        AllStatsPanel[2].SetValue_NotIncremental(character.DefensePoint);
        AllStatsPanel[3].SetValue_NotIncremental(character.Speed);

        foreach (KeyValuePair<BaseStats.Passive, float> kvp in character.PassiveList)
            AllPassivePanel[(int)kvp.Key - 1].SetValue_NotIncremental(kvp.Value, 1);

    }

    void Set_LevelUpPanel(Character character)
    {
        if (character == null)
            return;

        float currentScale = character.CurrentExp / character.NeedExp;
        CurrentExpSlider.fillAmount = currentScale;
        AddExpSlider.fillAmount = currentScale;
        CurrentExpSimulator = character.CurrentExp;
        NeedExpSimulator = character.NeedExp;


        CurrentExpProgessText.SetText(Math.Ceiling(character.CurrentExp) + "/" + Math.Ceiling(character.NeedExp));
        AddExpText.gameObject.SetActive(false);

        this.CurrentLevelSimulator = character.Level;
        this.Level.SetText(this.CurrentLevelSimulator.ToString());
        this.Level.color = Color.green;
    }

    void Set_PotionPanel()
    {
        SmallPotion.SetItem(DataManager.Instance.temporaryData.inventorys[Item.Type.SmallExpPotion]);
        MediumPotion.SetItem(DataManager.Instance.temporaryData.inventorys[Item.Type.MediumExpPotion]);
        LargePotion.SetItem(DataManager.Instance.temporaryData.inventorys[Item.Type.LargeExpPotion]);
        SuperPotion.SetItem(DataManager.Instance.temporaryData.inventorys[Item.Type.SuperExpPotion]);
    }

    public void Update_AddExpSliderUI(float AddingMount)
    {
        AddExpText.gameObject.SetActive(true);
        AddExpValue += AddingMount;
        AddExpText.SetText("(+ " + AddExpValue.ToString() + ")");

        CurrentExpSimulator += AddingMount;
        if (CurrentExpSimulator > NeedExpSimulator)
        {
            CurrentExpSlider.fillAmount = 0f;

            int LevelUpMount = (int)Math.Floor(Math.Log10((CurrentExpSimulator * 0.2) / NeedExpSimulator + 1) / Math.Log10(1.2));

            CurrentExpSimulator -= (float)(NeedExpSimulator * (Math.Pow(1.2f, LevelUpMount) - 1) / 0.2f);
            NeedExpSimulator *= (float)Math.Pow(1.2f, LevelUpMount);
            this.CurrentLevelSimulator += LevelUpMount;
            this.Level.SetText(this.CurrentLevelSimulator.ToString());
            this.Level.color = Color.green;

        }

        float currentScale = CurrentExpSimulator / NeedExpSimulator;
        AddExpSlider.fillAmount = currentScale;
    }
}
