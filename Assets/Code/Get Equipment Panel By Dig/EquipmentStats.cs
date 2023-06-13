using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
public class EquipmentStats : MonoBehaviour
{
    TextMeshProUGUI Name;

    List<TextMeshProUGUI> Stats = new List<TextMeshProUGUI>();
    List<GameObject> PassiveList = new List<GameObject>();
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


    public void SetStatsInSlot(Equipment equipment)
    {
        if (equipment == null)
        {
            Stats.Clear();
            PassiveList.Clear();
            gameObject.SetActive(false);

            return;
        }

        gameObject.SetActive(true);

        if (Stats.Count == 0)
        {
            Name = transform.Find("Name").GetComponent<TextMeshProUGUI>();

            for (int i = 1; i <= 4; i++)
            {
                Transform childStats = transform.GetChild(i);
                TextMeshProUGUI childValue = childStats.Find("Value").GetComponent<TextMeshProUGUI>();
                Stats.Add(childValue);
            }

            PassiveList.Add(transform.Find("Passive 1").gameObject);
            PassiveList.Add(transform.Find("Passive 2").gameObject);
        }

        Name.SetText("[" + equipment.quality + "] " + equipment.type);
        Name.color = QualityColor[(int)equipment.quality - 1];

        Stats[0].SetText(equipment.AttackDamage.ToString());
        Stats[1].SetText(equipment.HealthPoint.ToString());
        Stats[2].SetText(equipment.DefensePoint.ToString());
        Stats[3].SetText(equipment.Speed.ToString());

        for (int i = 0; i < 2; i++)
        {
            if(i >= equipment.PassiveList.Count)
            {
                PassiveList[i].SetActive(false);
                continue;
            }

            PassiveList[i].SetActive(true);

            TextMeshProUGUI PassiveName = PassiveList[i].transform.Find("Name").GetComponent<TextMeshProUGUI>();
            PassiveName.SetText(equipment.PassiveList.Keys.ElementAt(i).ToString());

            TextMeshProUGUI PassiveValue = PassiveList[i].transform.Find("Value").GetComponent<TextMeshProUGUI>();
            PassiveValue.SetText(equipment.PassiveList.Values.ElementAt(i).ToString());
        }

    }
    public List<TextMeshProUGUI> GetStatsText()
    {
        return Stats;
    }

    public List<float> GetStatsFloat()
    {
        if (Stats.Count == 0)
            return new List<float>() { 0f,0f,0f,0f};
        else
            return new List<float>()
            {
                (float)Convert.ToDouble(Stats[0].text),
                (float)Convert.ToDouble(Stats[1].text),
                (float)Convert.ToDouble(Stats[2].text),
                (float)Convert.ToDouble(Stats[3].text)
            };
    }
}
