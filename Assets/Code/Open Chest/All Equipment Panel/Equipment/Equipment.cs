using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Equipment : BaseStats
{
    public enum Type
    {
        None,
        Weapon,
        Helmet,
        Pauldron,
        Necklace,
        Vambrace,
        Gloves,
        Shield,
        Mask,
        Clothes,
        Pants,
        Belt,
        Shoes,
        Ring,

    }
    public Type type;

    public Sprite Icon;

    public enum Quality
    {
        None,
        Common, //White
        Uncommon, //Green
        Rare, //Blue
        Epic, //Violet
        Exotic, //Yellow
        Legendary, //Orange
        Mythic //red
    }
    public Quality quality;

    public int Level;

    public Equipment Clone()
    {
        Equipment e = new Equipment();

        e.type = this.type;
        e.Icon = this.Icon;
        e.quality = this.quality;
        e.Level = this.Level;
        e.AttackDamage = this.AttackDamage;
        e.HealthPoint = this.HealthPoint;
        e.DefensePoint = this.DefensePoint;
        e.Speed = this.Speed;
        foreach (KeyValuePair<Passive, float> kvp in PassiveList)
            e.PassiveList[kvp.Key] = kvp.Value;
        e.PowerPoint = this.PowerPoint;

        return e;
    }

    public string ToStringData()
    {
        string Stat = AttackDamage + "-" + HealthPoint + "-" + DefensePoint + "-" + Speed + "-" + (int)quality + "-" + Level;
        string Passive = "";
        foreach (KeyValuePair<Passive, float> kvp in PassiveList)
            Passive += "-" + (int)kvp.Key + "+" + kvp.Value;

        return Stat + Passive;

    }

    public Equipment ExtractStringData(int i, string DataReceive)
    {
        List<string> dataList = new List<string>(DataReceive.Split("-"));

        this.AttackDamage = (float)Convert.ToDouble(dataList[0]);
        this.HealthPoint = (float)Convert.ToDouble(dataList[1]);
        this.DefensePoint = (float)Convert.ToDouble(dataList[2]);
        this.Speed = (float)Convert.ToDouble(dataList[3]);
        this.type = (Equipment.Type)i;
        this.quality = (Equipment.Quality)Convert.ToInt16(dataList[4]);
        this.Level = Convert.ToInt16(dataList[5]);

        string LinkImage = "Equipment/" + this.type + "/" + this.quality;
        try
        {
            Texture2D texture = Resources.Load<Texture2D>(LinkImage);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            this.Icon = sprite;
        }
        catch (Exception)
        {
            Sprite sprite = Resources.Load<Sprite>(LinkImage);
            this.Icon = sprite;
        }

        for(int j = 0; j<dataList.Count-6; j++)
        {
            List<string> p = new List<String>(dataList[6 + j].Split("+"));
            BaseStats.Passive p0 = (BaseStats.Passive)(Convert.ToInt16(p[0]));
            float p1 = (float)Convert.ToDouble(p[1]);
            PassiveList[p0] = p1;
        }

        if (this.Level == 0)
            return null;
        else return this;
    }
}
