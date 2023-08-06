using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPanel : MonoBehaviour
{
    public RewardPanelUI rewardPanelUI;
    public GameObject InformVictoryPanel;
    public AllRewardPanel allRewardPanel;
    Chapter currentChapter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Chapter currentChapter)
    {
        InformVictoryPanel.SetActive(true);
        allRewardPanel.gameObject.SetActive(false);

        this.currentChapter = currentChapter;
        rewardPanelUI.SetReward(this.currentChapter.reward);

        this.currentChapter.IsDone = true;
        this.currentChapter.StarCount = 3;

        string n = this.currentChapter.Name.Split(" ")[1];
        Chapter nextChapter = Resources.Load<Chapter>("Chapter/Chapter " + n);
        nextChapter.IsOpen = true;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Open Chest");
    }

    public void ContinueButton()
    {
        InformVictoryPanel.SetActive(false);
        allRewardPanel.gameObject.SetActive(true);
        allRewardPanel.Initialize();
    }
}
