using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TranscendPowerPanel : MonoBehaviour
{
    List<StarPowerUI> starPowers = new List<StarPowerUI>();
    void SetAttr()
    {
        if (starPowers.Count == 0)
            for (int i = 1; i <= transform.childCount; i++)
                starPowers.Add(transform.Find(i + " Star Power Panel").GetComponent<StarPowerUI>());

    }

    public void SetStarPowerUI(List<string> description)
    {
        SetAttr();

        for (int i = 0; i < starPowers.Count; i++)
            if (i < description.Count && description[i] != null)
                starPowers[i].SetStarPowerUI(description[i]);
            else
                starPowers[i].SetStarPowerUI(null);
    }

    public void SetUpgradeStarUI(int currentStar)
    {
        SetAttr();

        for (int i = 0; i < starPowers.Count; i++)
            if (i < currentStar)
                starPowers[i].Upgraded();
            else
                starPowers[i].NotUpgradeYet();
    }
}
