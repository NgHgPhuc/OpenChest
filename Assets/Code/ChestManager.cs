using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestManager : MonoBehaviour
{
    public int CurrentLevel;

    public int MaxLevelEquipment;
    public int MinLevelEquipment;

    public List<float> TypeAccuracy = new List<float>();

    public List<float> QualityAccuracy = new List<float>();

    public Dictionary<string, Sprite> SkinCloset { get; private set; }

    public static ChestManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Equipment RandomEquipment()
    {
        Equipment equipment = new Equipment();
        equipment.Level = CurrentLevel;

        float PowerPoint = 0;

        Debug.Log("New Item");

        int Type = UnityEngine.Random.Range(1, 14);
        equipment.type = (Equipment.Type)Type;
        Debug.Log("Equipment Type: " + equipment.type);

        int Quality = UnityEngine.Random.Range(1, 8);
        equipment.quality = (Equipment.Quality)Quality;
        Debug.Log("Equipment Quality: " + equipment.quality);

        string LinkImage = (Equipment.Type)Type + "/" + (Equipment.Quality)Quality;
        try
        {
            Texture2D texture = Resources.Load<Texture2D>(LinkImage);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            equipment.Icon = sprite;
        }
        catch (Exception)
        {
            Sprite sprite = Resources.Load<Sprite>(LinkImage);
            equipment.Icon = sprite;
        }

        Debug.Log(LinkImage);

        equipment.AttackDamage = UnityEngine.Random.Range(1, 100);
        PowerPoint += equipment.AttackDamage*5;
        Debug.Log("AttackDamage: " + equipment.AttackDamage);

        equipment.HealthPoint = UnityEngine.Random.Range(1, 100);
        PowerPoint += equipment.HealthPoint * 2;
        Debug.Log("HealthPoint: " + equipment.HealthPoint);

        equipment.DefensePoint = UnityEngine.Random.Range(1, 100);
        PowerPoint += equipment.AttackDamage * 6;
        Debug.Log("DefensePoint: " + equipment.DefensePoint);

        equipment.Speed = UnityEngine.Random.Range(1, 100);
        PowerPoint += equipment.AttackDamage * 10;
        Debug.Log("Speed: " + equipment.Speed);

        int PassiveCount = UnityEngine.Random.Range(0, 3);
        Debug.Log("Have" + PassiveCount + "Passive");
        for (int i = 0; i < PassiveCount; i++)
        {
            int PassiveNum = UnityEngine.Random.Range(1, 6);

            float PassiveValue = UnityEngine.Random.Range(0f, 100f);
            PassiveValue = (float)Math.Round(PassiveValue, 2);
            PowerPoint += PassiveValue * 30;

            if (equipment.PassiveList.ContainsKey((Equipment.Passive)PassiveNum))
                equipment.PassiveList[(Equipment.Passive)PassiveNum] += PassiveValue;

            else equipment.PassiveList[(Equipment.Passive)PassiveNum] = PassiveValue;
        }

        foreach (var kvp in equipment.PassiveList)
            Debug.Log("Passive " + kvp.Key + ": " + kvp.Value);

        equipment.PowerPoint = PowerPoint;

        return equipment;
    }
}
