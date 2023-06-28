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

    List<Color> colors = new List<Color>()
    {
        new Color(240f/255, 240f/255, 240f/255),
        new Color(71f/255, 160f/255, 241f/255),
        new Color(255f/255,81f/255,222f/255),
        new Color(255f/255, 199f/255, 69f/255),
    };

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
        string paragraph = "Do you want to Level up your ally\nThis will consume all exp to do\nHappy Happy Happyy!";
        InformManager.Instance.Initialize_QuestionObject("Level Up", paragraph, LevelUp);
    }
    void LevelUp()
    {
        characterData.LevelUp();
        Set_LevelUpPanel();
        Set_StatsPanel();
        InformManager.Instance.Initialize_FloatingInform("Upgrade your ally's level successfully!");
    }

    public void SkillUpButton()
    {

    }

    public void TranscendButton()
    {
        if(characterData.StarCount >= 5)
        {
            InformManager.Instance.Initialize_FloatingInform("Your ally is full 5 Star!");
            return;
        }

        string paragraph = "Do you want to transcend your ally\nThis will make your ally more stronger!\nHappy Happy Happyy!";
        InformManager.Instance.Initialize_QuestionObject("Transcend", paragraph, Transcend);
    }
    void Transcend()
    {
        characterData.Transcend();
        Set_TranscendPanel();
        Set_StatsPanel();
        Set_InfomationPanel();
        InformManager.Instance.Initialize_FloatingInform("Transcend your ally successfully!");
    }

    public void SetDetail(Character character,int index)
    {
        if (character == null)
            return;

        this.characterData = character;
        this.index = index;

        Set_InfomationPanel();
        Set_CharacterPanel();
        Set_StatsPanel();
        Set_LevelUpPanel();
        Set_SkillPanel();
        Set_TranscendPanel();
        Set_IsCompain();

    }

    void Set_InfomationPanel()
    {
        if (character == null)
            return;

        for (int i = 0; i < StarList.childCount; i++)
            if (i < this.characterData.StarCount)
                StarList.GetChild(i).GetComponent<Image>().color = Color.white;
            else
                StarList.GetChild(i).GetComponent<Image>().color = Color.black;

        string tier = characterData.tier.ToString();
        Quality.SetText("[" + tier + "]");
        Quality.color = colors[(int)characterData.tier];

        Name.SetText(this.characterData.Name);
    }

    void Set_CharacterPanel()
    {
        if (this.character == null)
            return;

        this.character.sprite = this.characterData.Icon;
    }

    void Set_StatsPanel()
    {
        if(AllStatsPanel.Count == 0 || AllPassivePanel.Count == 0)
            for (int i = 0; i < statsPanel.childCount; i++)
                if (i < 4)
                    AllStatsPanel.Add(statsPanel.GetChild(i).GetComponent<SlotStatsPanel>());
                else
                    AllPassivePanel.Add(statsPanel.GetChild(i).GetComponent<SlotStatsPanel>());

        if (character == null)
            return;

        AllStatsPanel[0].SetValue_NotIncremental(this.characterData.AttackDamage);
        AllStatsPanel[1].SetValue_NotIncremental(this.characterData.HealthPoint);
        AllStatsPanel[2].SetValue_NotIncremental(this.characterData.DefensePoint);
        AllStatsPanel[3].SetValue_NotIncremental(this.characterData.Speed);

        foreach (KeyValuePair<BaseStats.Passive, float> kvp in this.characterData.PassiveList)
            AllPassivePanel[(int)kvp.Key - 1].SetValue_NotIncremental(kvp.Value, 1);

    }

    void Set_LevelUpPanel()
    {
        if (character == null)
            return;

        ExpBar.maxValue = this.characterData.NeedExp;
        ExpBar.value = this.characterData.CurrentExp;

        ExpProgessText.SetText(Math.Ceiling(this.characterData.CurrentExp) + "/" + Math.Ceiling(this.characterData.NeedExp));
        Level.SetText(this.characterData.Level.ToString());
    }

    void Set_SkillPanel()
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

    void Set_TranscendPanel()
    {
        if (character == null)
            return;

        ProgressSharp.SetText(this.characterData.CurrentSharp + "/" + this.characterData.NeedSharp);
        characterIcon.sprite = this.characterData.Icon;
    }

    void Set_IsCompain()
    {
        if (this.characterData.IsOwn == false)
        {
            CompainButtonText.transform.parent.gameObject.SetActive(false);
            return;
        }

        CompainButtonText.transform.parent.gameObject.SetActive(true);

        if (this.characterData.IsInTeam)
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
