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
    EnemyTeamPanel myTeamPanel;

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
        myTeamPanel = transform.Find("My Team Panel").GetComponent<EnemyTeamPanel>();
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

        this.chapter.MyTeam = new List<Character>(TeamManager.Instance.MyTeam());
        myTeamPanel.SetCharacterData(this.chapter.MyTeam);
    }

    public void NoneChapter()
    {
        gameObject.SetActive(false);
    }
}
