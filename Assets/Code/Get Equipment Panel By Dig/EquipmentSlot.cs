using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EquipmentSlot : MonoBehaviour
{
    Image BackgroundSlot;
    Image EquipmentImage;
    TextMeshProUGUI EquipmentLevel;
    List<Color> QualityColor = new List<Color>()
    {   
        new Color(240f/255, 240f/255, 240f/255), new Color(96f/255, 241f/255, 72f/255), //White,Green
        new Color(71f/255, 160f/255, 241f/255), new Color(255f/255,81f/255,222f/255),   //Blue, Purple
        new Color(255f/255, 255f/255, 102f/255), new Color(230f/255, 69f/255, 255f/255),//Yellow , Orange
        new Color(255f/255, 69f/255, 71f/255)                                            //Red
    };

    void Start()
    {

    }

    public void SetEquipmentInSlot(Equipment equipment)
    {
        if(equipment == null)
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

        this.EquipmentImage.sprite = equipment.Icon;
        this.EquipmentLevel.SetText("lv." + equipment.Level);
        BackgroundSlot.color = QualityColor[(int)equipment.quality-1];
    }

    public Color BackgroundColor()
    {
        return BackgroundSlot.color;
    }

}
