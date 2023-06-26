using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DetailAllyPanel : MonoBehaviour
{
    public Character characterData;
    public int index;

    [Header("Infomation Panel")]
    public Transform StarList;
    public TextMeshProUGUI Quality;
    public TextMeshProUGUI Name;

    [Header("Character Panel")]
    public Image character;

    [Header("Stats Panel")]
    public Transform statsPanel;
    List<SlotStatsPanel> AllStatsPanel = new List<SlotStatsPanel>();
    List<SlotStatsPanel> AllPassivePanel = new List<SlotStatsPanel>();

    [Header("Level Up Panel")]
    public Slider ExpBar;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI ExpProgessText;

    [Header("Skill Panel")]
    public Image SkillIcon;
    public TextMeshProUGUI SkillName;
    public TextMeshProUGUI SkillDescription;

    [Header("Transcend Panel")]
    public Image characterIcon;
    public TextMeshProUGUI ProgressSharp;

    [Header("Compain Panel")]
    public Image CompainIcon;
    public TextMeshProUGUI CompainButtonText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitButton()
    {
        AllySingleton.Instance.Initialize();
    }

    public void LevelUpButton()
    {

    }

    public void SkillUpButton()
    {

    }

    public void TranscendButton()
    {

    }


    public void SetDetail(Character character,int index)
    {
        if (character == null)
            return;

        this.characterData = character;
        this.index = index;

        Set_InfomationPanel(character);
        Set_CharacterPanel(character);
        Set_StatsPanel(character);
        Set_LevelUpPanel(character);
        Set_SkillPanel(character);
        Set_TranscendPanel(character);
        Set_IsCompain(character);

    }

    void Set_InfomationPanel(Character character)
    {
        if (character == null)
            return;

        for (int i = 0; i < StarList.childCount; i++)
            if (i < character.StarCount)
                StarList.GetChild(i).GetComponent<Image>().color = Color.white;
            else
                StarList.GetChild(i).GetComponent<Image>().color = Color.black;

        string quality = "Legendary";
        Quality.SetText("[" + quality + "]");

        Name.SetText(character.Name);
    }

    void Set_CharacterPanel(Character character)
    {
        if (character == null)
            return;

        this.character.sprite = character.Icon;
    }

    void Set_StatsPanel(Character character)
    {
        if(AllStatsPanel.Count == 0 || AllPassivePanel.Count == 0)
            for (int i = 0; i < statsPanel.childCount; i++)
                if (i < 4)
                    AllStatsPanel.Add(statsPanel.GetChild(i).GetComponent<SlotStatsPanel>());
                else
                    AllPassivePanel.Add(statsPanel.GetChild(i).GetComponent<SlotStatsPanel>());

        if (character == null)
            return;

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

        ExpBar.maxValue = character.NeedExp;
        ExpBar.value = character.CurrentExp;

        ExpProgessText.SetText(character.CurrentExp + "/" + character.NeedExp);
        Level.SetText(character.Level.ToString());
    }

    void Set_SkillPanel(Character character)
    {
        if (character == null)
            return;

        Sprite sIcon = SkillIcon.sprite;
        SkillIcon.sprite = sIcon;

        string sName = SkillName.text;
        SkillName.SetText(sName);

        string sDes = SkillDescription.text;
        SkillDescription.SetText(sDes);
    }

    void Set_TranscendPanel(Character character)
    {
        if (character == null)
            return;


    }

    void Set_IsCompain(Character character)
    {
        if(character.IsInTeam)
        {
            CompainIcon.gameObject.SetActive(true);
            CompainButtonText.SetText("Compain");
        }
        else
        {
            CompainIcon.gameObject.SetActive(false);
            CompainButtonText.SetText("Uncompain");
        }
    }

    public void LeftButtonClick()
    {
        if (index == 0)
            return;

        Character c = AllyOwnManager.Instance.GetAllAlly().ElementAt(index - 1).Value;
        SetDetail(c, index - 1);
    }

    public void RightButtonClick()
    {
        if (index == AllyOwnManager.Instance.GetAllAlly().Count - 1)
            return;

        Character c = AllyOwnManager.Instance.GetAllAlly().ElementAt(index + 1).Value;
        SetDetail(c, index + 1);
    }
}
