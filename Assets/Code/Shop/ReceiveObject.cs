using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class ReceiveObject : MonoBehaviour
{
    CharacterSlot characterSlot;
    SkillSlot skillSlot;
    TextMeshProUGUI Name;
    Image SharpIcon;
    void Start()
    {
        
    }

    public void SetReceiveCharacter(Character character)
    {
        if(characterSlot == null || Name == null || SharpIcon == null)
        {
            characterSlot = transform.Find("Ally 1 Slot Panel").GetComponent<CharacterSlot>();
            skillSlot = transform.Find("Ally 1 Slot Panel").GetComponent<SkillSlot>();
            Name = transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            SharpIcon = transform.Find("Sharp Icon").GetComponent<Image>();

        }

        if (character.IsOwn == false)
        {
            SharpIcon.gameObject.SetActive(false);
            characterSlot.gameObject.SetActive(true);
            Name.gameObject.SetActive(true);
            characterSlot.SetCharacterInSlot(character);
            this.Name.SetText(character.Name);
        }
        else
        {
            characterSlot.gameObject.SetActive(false);
            SharpIcon.gameObject.SetActive(true);
            Name.gameObject.SetActive(true);
            SharpIcon.sprite = character.Icon;
            string SharpReceive = (((int)character.tier + 1) * 10) + " Sharp";
            this.Name.SetText(SharpReceive);
        }

    }

    public void NoneReceive()
    {
        if (characterSlot == null || Name == null || SharpIcon == null)
        {
            characterSlot = transform.Find("Ally 1 Slot Panel").GetComponent<CharacterSlot>();
            Name = transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            SharpIcon = transform.Find("Sharp Icon").GetComponent<Image>();
        }

        SharpIcon.gameObject.SetActive(false);
        characterSlot.gameObject.SetActive(false);
        Name.gameObject.SetActive(false);
    }

    public void SetReceiveSkill(BaseSkill skill)
    {
        if (skillSlot == null || Name == null || SharpIcon == null)
        {
            skillSlot = transform.Find("Ally 1 Slot Panel").GetComponent<SkillSlot>();
            Name = transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            SharpIcon = transform.Find("Sharp Icon").GetComponent<Image>();

        }

        if (skill.IsHave == true)
        {
            characterSlot.gameObject.SetActive(false);
            SharpIcon.gameObject.SetActive(true);
            Name.gameObject.SetActive(true);
            SharpIcon.sprite = skill.Icon;
            string SharpReceive = skill.Name + " Sharp x20";
            this.Name.SetText(SharpReceive);

            skill.CurrentSharp += 20;
        }
        else
        {
            SharpIcon.gameObject.SetActive(false);
            characterSlot.gameObject.SetActive(true);
            Name.gameObject.SetActive(true);
            skillSlot.SetSkillInSlot(skill);
            this.Name.SetText(skill.Name);

            skill.IsHave = true;
        }

    }
}
