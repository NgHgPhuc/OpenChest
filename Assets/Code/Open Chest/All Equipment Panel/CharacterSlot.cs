using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSlot : MonoBehaviour,IPointerClickHandler
{
    Image BackgroundSlot;
    Image EquipmentImage;
    TextMeshProUGUI EquipmentLevel;
    public Character character;

    void Start()
    {

    }

    public void SetEquipmentInSlot()
    {
        if (character == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        if (EquipmentImage == null || EquipmentLevel == null || BackgroundSlot == null)
        {
            EquipmentImage = transform.Find("Equipment Image").GetComponent<Image>();
            EquipmentLevel = transform.Find("Equipment Level").GetComponent<TextMeshProUGUI>();
            BackgroundSlot = transform.Find("Background Slot").GetComponent<Image>();
        }

        this.EquipmentImage.sprite = character.Icon;
        this.EquipmentLevel.SetText("lv." + character.Level);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CharInfoPanel.Instance.Equip(character);
    }
}
