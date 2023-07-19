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

    public Transform InformationChapterPanel;
    RewardPanelUI rewardPanelUI;
    Image DoneIcon;

    public Transform FormationPanel;
    EnemyTeamPanel myTeamPanel;
    EnemyTeamPanel enemyTeamPanel;

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

        rewardPanelUI = InformationChapterPanel.Find("Reward Panel").GetComponent<RewardPanelUI>();
        DoneIcon = InformationChapterPanel.Find("Done Icon").GetComponent<Image>();

        enemyTeamPanel = FormationPanel.Find("Enemy Team Panel").GetComponent<EnemyTeamPanel>();
        myTeamPanel = FormationPanel.Find("My Team Panel").GetComponent<EnemyTeamPanel>();
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

    public void TurnTo_InfomationPanel()
    {
        InformationChapterPanel.gameObject.SetActive(true);
        FormationPanel.gameObject.SetActive(false);
    }
    public void TurnTo_FormationPanel()
    {
        InformationChapterPanel.gameObject.SetActive(false);
        FormationPanel.gameObject.SetActive(true);
    }
}
