using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarPowerUI : MonoBehaviour
{
    TextMeshProUGUI starLevel;
    TextMeshProUGUI descriptionLevel;
    Image star;

    void SetAttr()
    {
        if (starLevel == null)
            starLevel = transform.Find("Number").GetComponent<TextMeshProUGUI>();

        if (descriptionLevel == null)
            descriptionLevel = transform.Find("Description").GetComponent<TextMeshProUGUI>();

        if (star == null)
            star = transform.Find("Star 1").GetComponent<Image>();
    }

    public void SetStarPowerUI(string des)
    {
        if (des == null)
        {
            gameObject.SetActive(false);
            return;
        }

        SetAttr();

        gameObject.SetActive(true);
        descriptionLevel.SetText(des);
    }

    public void NotUpgradeYet()
    {
        SetAttr();

        Color notUp = new Color(65f / 255, 65f / 255, 65f / 255);
        starLevel.color = notUp;
        descriptionLevel.color = notUp;
        star.color = notUp;
    }

    public void Upgraded()
    {
        SetAttr();

        Color notUp = Color.white;
        starLevel.color = notUp;
        descriptionLevel.color = notUp;
        star.color = notUp;
    }
}
