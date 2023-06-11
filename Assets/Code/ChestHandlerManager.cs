using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHandlerManager : MonoBehaviour
{
    public static ChestHandlerManager Instance { get; private set; }
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


    public void RandomEquipment()
    {
        ChestManager.Instance.RandomEquipment();
    }
}
