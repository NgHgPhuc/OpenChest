using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(FightingUnit ally,float rewardExp = 40)
    {
        if (ally.CharacterClone != null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        this.character = ally.CharacterClone;

        characterIcon.sprite = this.character.Icon;
        currentExpScale.SetText(this.character.CurrentExp + "/" + this.character.NeedExp);
        ExpBarSlide.fillAmount = this.character.CurrentExp / this.character.NeedExp;

        this.rewardExpValue = rewardExp;
        this.rewardExpText.SetText(this.rewardExpValue.ToString());

        IEnumerator cal = CalculateExp(this.rewardExpValue / 20);
        StartCoroutine(cal);
    }

    IEnumerator CalculateExp(float gap)
    {
        while(this.rewardExpValue > 0)
        {
            this.rewardExpValue -= gap;
            this.rewardExpText.SetText("(+" + this.rewardExpValue.ToString() + ")");
            this.character.CurrentExp += gap;
            currentExpScale.SetText(this.character.CurrentExp + "/" + this.character.NeedExp);
            ExpBarSlide.fillAmount = this.character.CurrentExp / this.character.NeedExp;
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }
}
