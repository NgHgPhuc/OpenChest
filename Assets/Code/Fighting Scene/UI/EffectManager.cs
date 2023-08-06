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
        DeathEffect,
        BolaStrikes
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

    public void InitializeEffect(Transform pos,EffectName efName,float LivingTime,float Width,float Height)
    {
        SetAttr();
        GameObject g = effect[efName];
        Destroy(Instantiate(g, pos.position, pos.rotation, ShowPanel),LivingTime);

        RectTransform r = (RectTransform)g.transform;
        r.sizeDelta = new Vector2(Width, Height);
    }

    public void FloatingText(string msg,Color color,Transform pos, int Times)
    {
        FloatingObject fO = Instantiate(floatingObject, pos.position, pos.rotation, ShowPanel);
        fO.Iniatialize(msg, color);
        RectTransform size = (RectTransform)pos;
        fO.SetSize(size.rect.width, Times * size.rect.width / 4);
    }

    public void InitializeShooting(Transform FromPos, Transform ToPos, EffectName efName, float LivingTime, float Width, float Height)
    {
        SetAttr();
        GameObject gO = Instantiate(effect[efName], FromPos.position, FromPos.rotation, ShowPanel);
        Destroy(gO,LivingTime);

        RectTransform r = (RectTransform)gO.transform;
        r.sizeDelta = new Vector2(Width, Height);

        gO.transform.position = FromPos.position;
        Vector3 Distance = new Vector3(ToPos.position.x - gO.transform.position.x, ToPos.position.y - gO.transform.position.y, 0);
        float zRotate = (float)Math.Atan((Distance.y / Distance.x)) * 180 / 3.14f;
        gO.transform.Rotate(0,0,zRotate);
        IEnumerator sht = Shooting(gO, Distance, 15);
        StartCoroutine(sht);
    }

    IEnumerator Shooting(GameObject g, Vector3 Distance, int Times)
    {
        if (g == null)
            yield break;

        int i = 0;
        while (i < Times)
        {
            float gapX = Distance.x / Times;
            float DistanceX = g.transform.position.x + gapX;
            float gapY = Distance.y / Times;
            float DistanceY = g.transform.position.y + gapY;
            g.transform.position = new Vector3(DistanceX, DistanceY, 0);
            i++;
            yield return new WaitForSeconds(0.025f);
        }
        Destroy(g);
        yield break;
    }
}
