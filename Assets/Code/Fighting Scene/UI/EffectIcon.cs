using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EffectIcon : MonoBehaviour
{
    List<Image> EffectIconList = new List<Image>();
    List<TextMeshProUGUI> EffectIconText = new List<TextMeshProUGUI>();

    void Start()
    {
        
    }

    void SetAttr()
    {
        if (EffectIconList.Count > 0)
            return;

        for(int i = 0; i<10; i++)
        {
            EffectIconList.Add(transform.GetChild(i).GetComponentInChildren<Image>());
            EffectIconText.Add(transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBuffIcon(List<Buff> buffs)
    {
        SetAttr();

        for (int i = 0; i < 10; i++)
            if (i < buffs.Count)
            {
                EffectIconList[i].gameObject.SetActive(true);
                EffectIconText[i].gameObject.SetActive(true);

                EffectIconList[i].sprite = buffs[i].Icon;
                EffectIconText[i].SetText((buffs[i].duration).ToString());
            }
            else
            {
                EffectIconList[i].gameObject.SetActive(false);
                EffectIconText[i].gameObject.SetActive(false);
            }
    }
}
