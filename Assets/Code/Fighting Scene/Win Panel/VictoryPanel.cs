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

        allRewardPanel.Initialize(this.currentChapter);
        SetUI();
        OpenNextChapter();
        GetReward();

    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Open Chest");
    }

    public void ContinueButton()
    {
        InformVictoryPanel.SetActive(false);
        allRewardPanel.gameObject.SetActive(true);
    }

    //UI
    void SetUI()
    {
        rewardPanelUI.SetReward(this.currentChapter.reward);

        this.currentChapter.IsDone = true;
        this.currentChapter.StarCount = 3;
    }
    void OpenNextChapter()
    {
        string n = this.currentChapter.Name.Split(" ")[1];
        Chapter nextChapter = Resources.Load<Chapter>("Chapter/Chapter " + n);
        nextChapter.IsOpen = true;
    }

    void GetReward()
    {
        foreach (Reward r in currentChapter.reward)
            r.Earning();

        foreach(Character c in this.currentChapter.MyTeam)
            if (c != null && c.Name != "Player Fighting")
            {
                AllySO allySO = Resources.Load<AllySO>("Character/" + c.Name);
                allySO.character.AddCurrentExp(this.currentChapter.ExpForCharacter);
            }
    }
}
