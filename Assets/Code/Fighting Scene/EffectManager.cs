using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Linq;

public enum EffectPos
{
    OnTop,
    InLeft,
    InMiddle,
    InRight,
    AtBot
}
public class EffectTurn
{
    public GameObject effect;
    public int LivingTurn;
}

public class EffectManager : MonoBehaviour
{
    public enum EffectName
    {
        BeingAttack,
        Healing,
        TheLastLighting,
        TheDarknessBlade,
        PlayerStuning
    }

    public List<GameObject> EffectList;
    Dictionary<EffectName, GameObject> effect = new Dictionary<EffectName, GameObject>();

    public FloatingObject floatingObject;
    public Transform ShowPanel;

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
    }
    void SetAttr()
    {
        if (effect.Count <= 0)
            for (int i = 0; i < EffectList.Count; i++)
                effect.Add((EffectName)i, EffectList[i]);
    }

    public void InitializeEffect(Transform Place,EffectName efName,float LivingTime)
    {
        SetAttr();
        GameObject g = effect[efName];
        Destroy(Instantiate(g, Place.position, Place.rotation, Place),LivingTime);
    }

    public void InitializeEffect(Transform Place, EffectName efName)
    {
        GameObject g = effect[efName];
        Instantiate(g, Place.position, Place.rotation, Place);
    }

    public void FloatingText(string msg,Color color,Transform pos, int Times)
    {
        FloatingObject fO = Instantiate(floatingObject, pos.position, pos.rotation, ShowPanel);
        fO.Iniatialize(msg, color);
        RectTransform size = (RectTransform)pos;
        fO.SetSize(size.rect.width, Times * size.rect.width / 4);
    }

    //FloatingObject initializeFloatingObject(string msg, Color color, Transform pos,int Times)
    //{
    //    FloatingObject fO = Instantiate(floatingObject, pos.position, pos.rotation, ShowPanel);
    //    fO.Iniatialize(msg, color);
    //    RectTransform size = (RectTransform)pos;
    //    fO.SetSize(size.rect.width, Times * size.rect.width / 4);

    //    return fO;
    //}
}
