using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Equipment;

public class ChestManager : MonoBehaviour
{
    public int PlayerLevel;
    public int CurrentLevel;
    public int MaxLevel;

    public Dictionary<string, Sprite> SkinCloset { get; private set; }

    public static ChestManager Instance { get; private set; }

    LevelProbRandom CurrentLevelProb;
    LevelProbRandom NextLevelProb;
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
        CurrentLevelProb = FindCurrentLevel();
        NextLevelProb = FindCurrentLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Equipment RandomEquipment()
    {
        Equipment equipment = new Equipment();
        equipment.Level = PlayerLevel;

        equipment.type = RandomType();

        equipment.quality = RandomQuality();

        string LinkImage = equipment.type + "/" + equipment.quality;
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


        equipment.AttackDamage = RandomAttackDamage((int)equipment.quality);
        equipment.HealthPoint = RandomHealthDamage((int)equipment.quality);
        equipment.DefensePoint = RandomDefenseDamage((int)equipment.quality);
        equipment.Speed = RandomSpeedDamage((int)equipment.quality);


        int PassiveCount = UnityEngine.Random.Range(0, 3);
        for (int i = 0; i < PassiveCount; i++)
        {
            int PassiveNum = UnityEngine.Random.Range(1, 6);

            float PassiveValue = RandomPassiveValue((int)equipment.quality);

            if (equipment.PassiveList.ContainsKey((Equipment.Passive)PassiveNum))
                equipment.PassiveList[(Equipment.Passive)PassiveNum] += PassiveValue;

            else equipment.PassiveList[(Equipment.Passive)PassiveNum] = PassiveValue;
        }

        float PowerPoint = equipment.AttackDamage * 3 + equipment.HealthPoint * 1.5f + equipment.DefensePoint * 5 + equipment.Speed * 8;
        foreach (KeyValuePair<Passive, float> kvp in equipment.PassiveList)
            PowerPoint += kvp.Value * 30;
        equipment.PowerPoint = PowerPoint;

        return equipment;
    }

    Equipment.Type RandomType()
    {
        int Type = UnityEngine.Random.Range(1, 14);
        return (Equipment.Type)Type;
    }
    Equipment.Quality RandomQuality()
    {
        float r = UnityEngine.Random.Range(0f, 100f);

        int Quality = 0;
        for (int i=0; i < 7; i++)
        {
            if(r < CurrentLevelProb.GetSumRandomRate()[i])
            {
                Quality = i + 1;
                break;
            }
        }

        return (Equipment.Quality)Quality;
    }

    int RandomAttackDamage(int Quality)
    {
        int First = UnityEngine.Random.Range(4 * PlayerLevel, 8 * PlayerLevel);
        int Second = UnityEngine.Random.Range(1, 5) * CurrentLevel;
        int Third = (int)(First * Quality / 7f);
        return First + Second + Third;
    }
    int RandomHealthDamage(int Quality)
    {
        int First = UnityEngine.Random.Range(15 * PlayerLevel, 25 * PlayerLevel);
        int Second = UnityEngine.Random.Range(1, 5) * CurrentLevel;
        int Third = (int)(First * Quality / 7f);
        return First + Second + Third;
    }
    int RandomDefenseDamage(int Quality)
    {
        int First = UnityEngine.Random.Range(1 * PlayerLevel, 5 * PlayerLevel);
        int Second = UnityEngine.Random.Range(1, 5) * CurrentLevel;
        int Third = (int)(First * Quality / 7f);
        return First + Second + Third;
    }
    int RandomSpeedDamage(int Quality)
    {
        int First = UnityEngine.Random.Range(1 * PlayerLevel, 3 * PlayerLevel);
        int Second = UnityEngine.Random.Range(1, 5) * CurrentLevel;
        int Third = (int)(First * Quality / 7f);
        return First + Second + Third;
    }

    float RandomPassiveValue(int Quality)
    {
        float PassiveValue = UnityEngine.Random.Range(0f* Quality, 2f* Quality);
        PassiveValue = (float)Math.Round(PassiveValue, 2);
        print(PassiveValue);
        return PassiveValue;
    }

    public LevelProbRandom FindCurrentLevel()
    {
        string LinkData = "ScriptableObject/Level "+ CurrentLevel;
        return Resources.Load<LevelProbRandom>(LinkData);
    }
    public LevelProbRandom FindNextLevel()
    {
        int NextLevel = CurrentLevel + 1;
        if (NextLevel > MaxLevel) NextLevel = MaxLevel;
        string LinkData = "ScriptableObject/Level " + NextLevel;
        return Resources.Load<LevelProbRandom>(LinkData);
    }

    public void UpgradeChest()
    {
        if (!ResourceManager.Instance.CheckEnought_Gold(CurrentLevelProb.Cost))
            return;

        if (CurrentLevel >= MaxLevel)
            return;

        ResourceManager.Instance.ChangeGold(CurrentLevelProb.Cost);
        CurrentLevel += 1;
        CurrentLevelProb = FindCurrentLevel();
        NextLevelProb = FindNextLevel();
    }
}
