using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPanel : MonoBehaviour
{
    public RewardPanelUI rewardPanelUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Chapter currentChapter)
    {
        rewardPanelUI.SetRewardList(currentChapter.reward);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Open Chest");
    }
}
