using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DetailAllyPanel : MonoBehaviour
{
    public Character character;
    public int index;

    [Header("Infomation Panel")]
    public Transform StarList;
    public TextMeshProUGUI Quality;
    public TextMeshProUGUI Name;

    [Header("Character Panel")]
    public Image characterAvatar;
    public Animator characterAnimator;

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

    [Header("Tier Effect")]
    public GameObject TierEffect;
    public Image TierEffect_Image;
    public Animator TierEffect_Animation;

    [Header("Unlock")]
    public GameObject UnlockButton;
    
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
        character.LevelUp();
        Set_LevelUpPanel();
        Set_StatsPanel();

        DataManager.Instance.SaveData(character.Name, character.ToStringData());

        InformManager.Instance.Initialize_FloatingInform("Upgrade your ally's level successfully!");
    }

    public void SkillUpButton()
    {

    }

    public void TranscendButton()
    {
        if(character.StarCount >= 5)
        {
            InformManager.Instance.Initialize_FloatingInform("Your ally is full 5 Star!");
            return;
        }

        string paragraph = "Do you want to transcend your ally\nThis will make your ally more stronger!\nHappy Happy Happyy!";
        InformManager.Instance.Initialize_QuestionObject("Transcend", paragraph, Transcend);
    }
    void Transcend()
    {
        character.Transcend();
        Set_TranscendPanel();
        Set_StatsPanel();
        Set_InfomationPanel();

        DataManager.Instance.SaveData(character.Name, character.ToStringData());

        InformManager.Instance.Initialize_FloatingInform("Transcend your ally successfully!");
    }



    public void SetDetail(Character character,int index)
    {
        if (character == null)
            return;

        this.character = character;
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
        if (characterAvatar == null)
            return;

        for (int i = 0; i < StarList.childCount; i++)
            if (i < this.character.StarCount)
                StarList.GetChild(i).GetComponent<Image>().color = Color.white;
            else
                StarList.GetChild(i).GetComponent<Image>().color = Color.black;

        string tier = character.tier.ToString();
        Quality.SetText("[" + tier + "]");
        Quality.color = character.GetColor();

        Name.SetText(this.character.Name);

        if ((int)character.tier >= 2)
        {
            TierEffect.SetActive(true);
            TierEffect_Image.color = character.GetColor();
        }
        else
            TierEffect.SetActive(false);
    }

    void Set_CharacterPanel()
    {
        if (this.characterAvatar == null)
            return;

        this.characterAvatar.sprite = this.character.Icon;
        if (this.character.IsOwn == false)
            this.characterAvatar.color = new Color(120f / 255, 120f / 255, 120f / 255);
        else this.characterAvatar.color = Color.white;
        this.characterAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/" + this.character.Name +"/"+ this.character.Name);
    }

    void Set_StatsPanel()
    {
        if(AllStatsPanel.Count == 0 || AllPassivePanel.Count == 0)
            for (int i = 0; i < statsPanel.childCount; i++)
                if (i < 4)
                    AllStatsPanel.Add(statsPanel.GetChild(i).GetComponent<SlotStatsPanel>());
                else
                    AllPassivePanel.Add(statsPanel.GetChild(i).GetComponent<SlotStatsPanel>());

        if (characterAvatar == null)
            return;

        AllStatsPanel[0].SetValue_NotIncremental(this.character.AttackDamage);
        AllStatsPanel[1].SetValue_NotIncremental(this.character.HealthPoint);
        AllStatsPanel[2].SetValue_NotIncremental(this.character.DefensePoint);
        AllStatsPanel[3].SetValue_NotIncremental(this.character.Speed);

        foreach (KeyValuePair<BaseStats.Passive, float> kvp in this.character.PassiveList)
            AllPassivePanel[(int)kvp.Key - 1].SetValue_NotIncremental(kvp.Value, 1);

    }

    void Set_LevelUpPanel()
    {
        if (characterAvatar == null)
            return;

        ExpBar.maxValue = this.character.NeedExp;
        ExpBar.value = this.character.CurrentExp;

        ExpProgessText.SetText(Math.Ceiling(this.character.CurrentExp) + "/" + Math.Ceiling(this.character.NeedExp));
        Level.SetText(this.character.Level.ToString());
    }

    void Set_SkillPanel()
    {
        if (characterAvatar == null)
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
        if (characterAvatar == null)
            return;

        ProgressSharp.SetText(this.character.CurrentSharp + "/" + this.character.NeedSharp);
        characterIcon.sprite = this.character.Icon;
    }

    void Set_IsCompain()
    {
        if (this.character.IsOwn == false)
        {
            CompainIcon.gameObject.SetActive(false);
            CompainButtonText.transform.parent.gameObject.SetActive(false);//button off

            UnlockButton.SetActive(true);
            return;
        }

        UnlockButton.SetActive(false);
        CompainButtonText.transform.parent.gameObject.SetActive(true);

        if (!this.character.IsInTeam)
        {
            CompainIcon.gameObject.SetActive(false);
            CompainButtonText.SetText("Compain");
        }
        else
        {
            CompainIcon.gameObject.SetActive(true);
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
