using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPanel : MonoBehaviour
{
    public RewardPanelUI rewardPanelUI;
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
        this.currentChapter = currentChapter;
        rewardPanelUI.SetReward(this.currentChapter.reward);
    }

    void GetReward(Reward reward)
    {

    }

    public void ExitButton()
    {
        this.currentChapter.IsDone = true;
        this.currentChapter.StarCount = 3;

        string n = this.currentChapter.Name.Split(" ")[1];
        Chapter nextChapter = Resources.Load<Chapter>("Chapter/Chapter " + n);
        nextChapter.IsOpen = true;

        SceneManager.LoadScene("Open Chest");
    }
}
