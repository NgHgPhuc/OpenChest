using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class CharacterSlot : MonoBehaviour,IPointerClickHandler
{
    Image BackgroundSlot;
    Image EquipmentImage;
    Image AddIcon;
    Image LockIcon;
    TextMeshProUGUI EquipmentLevel;
    StarListPanel StarList;
    public Character character;

    void Start()
    {

    }

    public void SetCharacterInSlot(Character character)
    {
        if (character == null)
        {
            DontHaveCharacterInSlot();
            return;
        }
        this.character = character.Clone();

        SetAttr();

        this.EquipmentImage.sprite = this.character.Avatar;
        this.EquipmentLevel.SetText(this.character.Name);
        this.StarList.SetStarCount(this.character.currentStarCount, this.character.maxStarCount, 5);
        if (AddIcon != null)
            this.AddIcon.gameObject.SetActive(false);
    }

    public void DontHaveCharacterInSlot()
    {
        SetAttr();

        this.character = null;
        gameObject.SetActive(true);
        this.EquipmentImage.sprite = null;
        this.EquipmentLevel.SetText("");
        if (AddIcon != null)
            this.AddIcon.gameObject.SetActive(true);
        StarList.SetStarCount(0, 0, 5);
    }

    public void GatchaCharacter(Character character)
    {
        if (character == null)
        {
            gameObject.SetActive(false);
            return;
        }
        this.character = character.Clone();
        gameObject.SetActive(true);

        SetAttr();

        this.EquipmentImage.sprite = this.character.Avatar;
        this.EquipmentLevel.SetText(this.character.Name);
        this.StarList.SetStarCount(this.character.currentStarCount, this.character.maxStarCount, 5);
        if (AddIcon != null)
            this.AddIcon.gameObject.SetActive(false);
    }

    public void HaventOpenSlotYet()//level not enough
    {

    }
    void SetAttr()
    {
        if (EquipmentImage == null)
            EquipmentImage = transform.Find("Equipment Image").GetComponent<Image>();

        if (EquipmentLevel == null)
            EquipmentLevel = transform.Find("Equipment Level").GetComponent<TextMeshProUGUI>();

        if (BackgroundSlot == null)
            BackgroundSlot = transform.Find("Background Slot").GetComponent<Image>();

        if (AddIcon == null)
            if(transform.Find("Add Icon") != null)
                AddIcon = transform.Find("Add Icon").GetComponent<Image>();

        if(LockIcon == null)
            if (transform.Find("Lock Icon") != null)
                LockIcon = transform.Find("Lock Icon").GetComponent<Image>();

        if (StarList == null)
            StarList = transform.Find("Star Panel").GetComponent<StarListPanel>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.character == null)
        {
            WardPanel.Instance.AllyWarp();
            return;
        }

        CharInfoPanel.Instance.ShowInfo(this.character);
    }

}
