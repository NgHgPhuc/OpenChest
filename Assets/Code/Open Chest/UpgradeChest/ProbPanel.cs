using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ProbPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Button UpgradeButton;
    public Transform LevelPanel;
    public TextMeshProUGUI CostText;

    void Start()
    {
        UpgradeButton.onClick.AddListener(UpgradeFunc);
        ShowProb();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShowValueOfChild(Transform child,float currentProb,float nextProb)
    {
        child.Find("Current Level Prob Value").GetComponent<TextMeshProUGUI>().SetText(currentProb + "%");
        child.Find("Next Level Prob Value").GetComponent<TextMeshProUGUI>().SetText(nextProb + "%");
    }

    void SetValue(LevelProbRandom currentLevelProb, LevelProbRandom nextLevelProb)
    {
        int c = currentLevelProb.RandomRate.Count;
        for (int i = 0; i < c; i++)
        {
            ShowValueOfChild(transform.GetChild(i), currentLevelProb.RandomRate[i], nextLevelProb.RandomRate[i]);
        }
    }

    void ShowProb()
    {
        LevelProbRandom currentLevelProb = ChestManager.Instance.FindCurrentLevel();
        LevelProbRandom nextLevelProb = ChestManager.Instance.FindNextLevel();
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
        transform.parent.parent.gameObject.SetActive(false);
    }
}
