using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InfomationFighting : MonoBehaviour,IPointerDownHandler
{
    public GameObject InfomationObject;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Cooldown;
    public TextMeshProUGUI Description;
    public Image SkillIcon;

    public static InfomationFighting Instance { get; private set; }
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

    public void Initialize(BaseSkill skill,Vector3 Pos)
    {
        InfomationObject.SetActive(true);
        SetSkillInfo(skill);
        SetPosInfo(Pos);
    }

    void SetSkillInfo(BaseSkill skill)
    {
        Name.SetText(skill.Name);
        Cooldown.SetText("CD: " + skill.Cooldown + " Turns");
        Description.SetText(skill.Description);
        SkillIcon.sprite = skill.Icon;
    }

    void SetPosInfo(Vector3 Pos)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InfomationObject.SetActive(false);
    }
}
