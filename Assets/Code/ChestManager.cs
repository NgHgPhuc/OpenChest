using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public int CurrentLevel;

    public int MaxLevelEquipment;
    public int MinLevelEquipment;

    public List<float> TypeAccuracy = new List<float>();

    public List<float> QualityAccuracy = new List<float>();

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
        Debug.Log("New Item");
        int Type = UnityEngine.Random.Range(1, 13);
        Debug.Log("Equipment Type: " + (Equipment.Type)Type);

        int Quality = UnityEngine.Random.Range(1, 8);
        Debug.Log("Equipment Quality: " + (Equipment.Quality)Quality);

        equipment.AttackDamage = UnityEngine.Random.Range(1, 100);
        Debug.Log("AttackDamage: " + equipment.AttackDamage);
        equipment.HealthPoint = UnityEngine.Random.Range(1, 100);
        Debug.Log("HealthPoint: " + equipment.HealthPoint);
        equipment.DefensePoint = UnityEngine.Random.Range(1, 100);
        Debug.Log("DefensePoint: " + equipment.DefensePoint);
        equipment.Speed = UnityEngine.Random.Range(1, 100);
        Debug.Log("Speed: " + equipment.Speed);

        int PassiveCount = UnityEngine.Random.Range(0, 3);
        Debug.Log("Have" + PassiveCount + "Passive");
        for (int i = 0; i < PassiveCount; i++)
        {
            int PassiveNum = UnityEngine.Random.Range(1, 6);
            float PassiveValue = UnityEngine.Random.Range(0f, 100f);

            if (equipment.PassiveList.ContainsKey((Equipment.Passive)PassiveNum))
                equipment.PassiveList[(Equipment.Passive)PassiveNum] += PassiveValue;

            else equipment.PassiveList[(Equipment.Passive)PassiveNum] = PassiveValue;
        }

        foreach (var kvp in equipment.PassiveList)
            Debug.Log("Passive " + kvp.Key + ": " + kvp.Value);

        return equipment;
    }
}
