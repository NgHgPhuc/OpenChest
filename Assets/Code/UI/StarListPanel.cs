using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarListPanel : MonoBehaviour
{
    List<Image> StarList = new List<Image>();
    void Start()
    {
        SetAttr();
    }

    void SetAttr()
    {
        if (StarList.Count > 0)
            return;

        for (int i = 1; i <= 3; i++)
            StarList.Add(transform.Find("Star "+i).GetComponent<Image>());
    }

    public void SetStarCount(int StarCount)
    {
        SetAttr();

        for (int i = 0; i < 3; i++)
            if (i < StarCount)
                StarList[i].color = Color.white;
            else
                StarList[i].color = Color.black;
    }
}
