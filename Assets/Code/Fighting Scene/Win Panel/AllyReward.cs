using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class AllyReward : MonoBehaviour
{
    Character character;
    public Image characterIcon;
    public TextMeshProUGUI currentExpScale;
    public TextMeshProUGUI rewardExpText;
    public Image ExpBarSlide;
    public float rewardExpValue;

    void Start()
    {
        EarnExpAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Character ally,float rewardExp)
    {
        if (ally == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        this.character = ally.Clone();

        characterIcon.sprite = this.character.Avatar;
        currentExpScale.SetText(Math.Ceiling(this.character.CurrentExp) + "/" + Math.Ceiling(this.character.NeedExp));
        ExpBarSlide.fillAmount = this.character.CurrentExp / this.character.NeedExp;

        this.rewardExpValue = rewardExp;
        this.rewardExpText.SetText("(+" + Math.Ceiling(this.rewardExpValue) + ")");
    }

    public void EarnExpAnimation()
    {
        IEnumerator cal = CalculateExp(this.rewardExpValue / 20);
        StartCoroutine(cal);
    }
    IEnumerator CalculateExp(float gap)
    {
        while (this.rewardExpValue > 0)
        {
            this.rewardExpValue -= gap;
            this.rewardExpText.SetText("(+" + Math.Ceiling(this.rewardExpValue) + ")");
            this.character.AddCurrentExp(gap);
            currentExpScale.SetText(this.character.CurrentExp + "/" + this.character.NeedExp);
            ExpBarSlide.fillAmount = this.character.CurrentExp / this.character.NeedExp;
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }
}
