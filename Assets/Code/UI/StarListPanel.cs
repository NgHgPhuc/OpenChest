using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarListPanel : MonoBehaviour
{
    List<Image> StarList = new List<Image>();

    void SetAttr(int MaxStarInUI)
    {
        if (StarList.Count > 0)
            return;

        for (int i = 1; i <= MaxStarInUI; i++)
            StarList.Add(transform.Find("Star "+i).GetComponent<Image>());
    }

    public void SetStarCount(int CurrentStarCount,int MaxStarCount,int MaxStarInUI)
    {
        SetAttr(MaxStarInUI);

        for (int i = 0; i < MaxStarInUI; i++)
        {
            if(i < MaxStarCount)
                StarList[i].gameObject.SetActive(true);
            else StarList[i].gameObject.SetActive(false);

            if (i < CurrentStarCount)
                StarList[i].color = Color.white;
            else
                StarList[i].color = Color.black;
        }
    }
}
