using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;
using UnityEngine.SceneManagement;

public class ChapterPanelUI : MonoBehaviour
{
    Chapter chapter;

    TextMeshProUGUI ChapterName;
    EnemyTeamPanel enemyTeamPanel;
    RewardPanelUI rewardPanelUI;
    EnemyTeamPanel myTeamPanel;
    Image DoneIcon;

    CanvasGroup canvasGroup;

    public RewardPanelUI GetClearItem;

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
        DoneIcon = transform.Find("Done Icon").GetComponent<Image>();

        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void SetChapterInfomation(Chapter chapter)
    {
        if (this.chapter == null)
            SetAttr();

        this.chapter = chapter;

        gameObject.SetActive(true);

        this.ChapterName.SetText(this.chapter.name);

        enemyTeamPanel.SetCharacterData(this.chapter.EnemyTeam);

        rewardPanelUI.SetRewardList(this.chapter.reward, this.chapter.StarCount);

        this.chapter.MyTeam = new List<Character>(TeamManager.Instance.MyTeam());
        myTeamPanel.SetCharacterData(this.chapter.MyTeam);

        DoneIcon.gameObject.SetActive(chapter.IsDone);
    }

    public void NoneChapter()
    {
        gameObject.SetActive(false);
    }

    public void EnterButton()
    {
        PlayerPrefs.SetString("Current Chapter Name", ChapterName.text);
        SceneManager.LoadScene("Fighting");
    }

    public void ClearButton()
    {
        if (chapter.IsDone == false)
        {
            InformManager.Instance.Initialize_FloatingInform("You didn't clear this chapter");
            return;
        }

        if (!TicketClearPanel.Instance.IsEnoughTicket(1))
        {
            InformManager.Instance.Initialize_FloatingInform("You do not enough ticket");
            return;
        }

        TicketClearPanel.Instance.UsingTicket(-1);

        GetClearItem.transform.parent.gameObject.SetActive(true);
        GetClearItem.SetReward(chapter.reward);
        foreach (Reward r in chapter.reward)
            r.Earning();
    }
}
