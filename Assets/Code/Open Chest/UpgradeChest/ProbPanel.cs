using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Globalization;

public class ProbPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Button UpgradeButton;
    public Transform LevelPanel;
    public TextMeshProUGUI CostText;

    public GameObject Parent;

    List<TextMeshProUGUI> ShowValueList = new List<TextMeshProUGUI>();
    void Start()
    {
        UpgradeButton.onClick.AddListener(UpgradeFunc);
        ShowProb();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShowValueOfChild(int i,string currentProb,string nextProb)
    {
        if(ShowValueList.Count / 2 < i + 1)
        {
            ShowValueList.Add(transform.GetChild(i).Find("Current Level Prob Value").GetComponent<TextMeshProUGUI>());
            ShowValueList.Add(transform.GetChild(i).Find("Next Level Prob Value").GetComponent<TextMeshProUGUI>());
        }

        ShowValueList[i * 2].SetText(currentProb + "%");
        ShowValueList[i * 2 + 1].SetText(nextProb + "%");
    }

    void SetValue(LevelProbRandom currentLevelProb, LevelProbRandom nextLevelProb)
    {
        int c = currentLevelProb.RandomRate.Count;
        for (int i = 0; i < c; i++)
        {
            string currentProb = currentLevelProb.RandomRate[i].ToString(CultureInfo.InvariantCulture);
            string nextProb = nextLevelProb.RandomRate[i].ToString(CultureInfo.InvariantCulture);
            ShowValueOfChild(i, currentProb, nextProb);
        }

        string CurrentDamageRange = currentLevelProb.DamageChestRange[0].ToString() + " ~ " + currentLevelProb.DamageChestRange[1].ToString();
        string NextDamageRange = nextLevelProb.DamageChestRange[0].ToString() + " ~ " + nextLevelProb.DamageChestRange[1].ToString();
        ShowValueOfChild(c, CurrentDamageRange, NextDamageRange);

        string CurrentHpRange = currentLevelProb.ChestHpRange[0].ToString() + " ~ " + currentLevelProb.ChestHpRange[1].ToString();
        string NextHpRange = nextLevelProb.ChestHpRange[0].ToString() + " ~ " + nextLevelProb.ChestHpRange[1].ToString();
        ShowValueOfChild(c + 1, CurrentDamageRange, NextDamageRange);
    }

    void ShowProb()
    {
        LevelProbRandom currentLevelProb = ChestManager.Instance.CurrentLevelProb;
        LevelProbRandom nextLevelProb = ChestManager.Instance.NextLevelProb;
        SetValue(currentLevelProb, nextLevelProb);

        int currentLevel = ChestManager.Instance.CurrentLevel;
        LevelPanel.Find("Current Level").GetComponent<TextMeshProUGUI>().SetText("Lv."+currentLevel);
        LevelPanel.Find("Next Level").GetComponent<TextMeshProUGUI>().SetText("Lv." + (currentLevel+1));

        CostText.SetText(nextLevelProb.Cost.ToString());
    }

    void UpgradeFunc()
    {
        ChestManager.Instance.UpgradeChest();
        ShowProb();
    }

    public void ClosePanel()
    {
        Parent.SetActive(false);
    }
}
