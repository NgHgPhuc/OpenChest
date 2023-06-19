using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;

public class ChapterPanelUI : MonoBehaviour
{
    Chapter chapter;

    TextMeshProUGUI ChapterName;
    EnemyTeamPanel enemyTeamPanel;
    RewardPanelUI rewardPanelUI;


    void Start()
    {
        SetAttr();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetAttr()
    {
        ChapterName = transform.Find("Chapter Name Panel").GetChild(0).GetComponent<TextMeshProUGUI>();
        enemyTeamPanel = transform.Find("Enemy Team Panel").GetComponent<EnemyTeamPanel>();
        rewardPanelUI = transform.Find("Reward Panel").GetComponent<RewardPanelUI>();
    }
    public void SetChapterInfomation(Chapter chapter)
    {
        if (this.chapter == null)
            SetAttr();

        this.chapter = chapter;
        gameObject.SetActive(true);
        this.ChapterName.SetText(this.chapter.name);
        enemyTeamPanel.SetCharacterData(this.chapter.EnemyTeam);
        rewardPanelUI.SetRewardList(this.chapter.reward);
    }

    public void NoneChapter()
    {
        gameObject.SetActive(false);
    }
}
