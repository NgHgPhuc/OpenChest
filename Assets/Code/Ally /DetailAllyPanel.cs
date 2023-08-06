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

    public AllyInfomationUI allyInfomationUI;
    public StatAndLevelUI statAndLevelUI;
    public SkillAllyPanel skillAllyPanel;
    public TranscendAllyPanel transcendAllyPanel;

    Dictionary<Item.Type, int> ExpPotionUsing = new Dictionary<Item.Type, int>()
    {
        { Item.Type.SmallExpPotion,0},
        { Item.Type.MediumExpPotion,0},
        { Item.Type.LargeExpPotion,0},
        { Item.Type.SuperExpPotion,0}
    };
    int AddExpValue = 0;



    public void SetDetail(Character character, int index)
    {
        if (character == null)
            return;

        this.character = character;
        this.index = index;

        allyInfomationUI.SetAllyUI(this.character);
        statAndLevelUI.SetAllyUI(this.character);
        skillAllyPanel.SetSkillList(this.character.skill);
        transcendAllyPanel.SetTranscendUI(this.character);

        ResetValueUsing();
    }




    public void ExitButton()
    {
        AllySingleton.Instance.Initialize();
    }

    public void UsingExpPotion(Item.Type expPotionType, int addExpValue)
    {
        ExpPotionUsing[expPotionType] += 1;
        this.AddExpValue += addExpValue;
        this.statAndLevelUI.Update_AddExpSliderUI(addExpValue);
    }

    public void LevelUpButton()
    {
        if(AddExpValue == 0)
        {
            string paragraph = "You haven't use any potion yet!";
            InformManager.Instance.Initialize_FloatingInform(paragraph);
        }
        else
        {
            string paragraph = "Are you sure to use all of that below potion\nto this ally?";
            InformManager.Instance.Initialize_QuestionObject("Level Up", paragraph, AllyGetExp);
        }
    }

    void AllyGetExp()
    {
        this.character.CurrentExp += this.AddExpValue;
        DataManager.Instance.SaveData(character.Name, character.ToStringData());
        statAndLevelUI.SetAllyUI(this.character);

        foreach (KeyValuePair<Item.Type, int> kvp in ExpPotionUsing)
            DataManager.Instance.temporaryData.ChangeValue(kvp.Key, kvp.Value, TemporaryData.ChangeType.USING);

        ResetValueUsing();


        if (this.character.CheckCanLevelUp())
            LevelUp();
    }
    void ResetValueUsing()
    {
        ExpPotionUsing = new Dictionary<Item.Type, int>()
        {
            { Item.Type.SmallExpPotion,0},
            { Item.Type.MediumExpPotion,0},
            { Item.Type.LargeExpPotion,0},
            { Item.Type.SuperExpPotion,0}
        };
        AddExpValue = 0;
    }
    void LevelUp()
    {
        this.character.LevelUp();
        statAndLevelUI.SetAllyUI(this.character);

        allyInfomationUI.LevelUpEffect();

        DataManager.Instance.SaveData(character.Name, character.ToStringData());

        InformManager.Instance.Initialize_FloatingInform("Upgrade your ally's level successfully!");
    }


    public void SkillUpButton()
    {

    }

    public void TranscendButton()
    {
        //if(character.currentStarCount >= 5)
        //{
        //    InformManager.Instance.Initialize_FloatingInform("Your ally is full 5 Star!");
        //    return;
        //}

        //string paragraph = "Do you want to transcend your ally\nThis will make your ally more stronger!\nHappy Happy Happyy!";
        //InformManager.Instance.Initialize_QuestionObject("Transcend", paragraph, Transcend);
    }
    void Transcend()
    {
        //character.Transcend();
        //Set_TranscendPanel();
        //Set_StatsPanel();
        //Set_InfomationPanel();

        //DataManager.Instance.SaveData(character.Name, character.ToStringData());

        //InformManager.Instance.Initialize_FloatingInform("Transcend your ally successfully!");
    }

    public void LeftButtonClick()
    {
        if (index == 0)
        {
            InformManager.Instance.Initialize_FloatingInform("Dont have any ally in this side!");
            return;
        }

        Character c = AllyOwnManager.Instance.GetAllAlly().ElementAt(index - 1).Value;
        SetDetail(c, index - 1);
    }

    public void RightButtonClick()
    {
        if (index == AllyOwnManager.Instance.GetAllAlly().Count - 1)
        {
            InformManager.Instance.Initialize_FloatingInform("Dont have any ally in this side!");
            return;
        }

        Character c = AllyOwnManager.Instance.GetAllAlly().ElementAt(index + 1).Value;
        SetDetail(c, index + 1);
    }
}
