using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EffectPos
{
    OnTop,
    InLeft,
    InMiddle,
    InRight,
    AtBot
}
public class EffectManager : MonoBehaviour
{
    public enum EffectName
    {
        BeingAttack,
        Healing,
        TheLastLighting,
        TheDarknessBlade
    }

    public List<GameObject> EffectList;
    Dictionary<EffectName, GameObject> effect = new Dictionary<EffectName, GameObject>();

    public static EffectManager Instance;
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

    private void Start()
    {
        for (int i = 0; i < EffectList.Count; i++)
            effect.Add((EffectName)i, EffectList[i]);
    }

    public void InitializeEffect(Transform Place,EffectName efName,float LivingTime)
    {
        GameObject g = effect[efName];
        Destroy(Instantiate(g, Place.position, Place.rotation, Place),LivingTime);
    }
}
