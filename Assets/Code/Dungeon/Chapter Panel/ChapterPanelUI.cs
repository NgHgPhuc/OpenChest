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

    public Transform InformationChapterPanel;
    RewardPanelUI rewardPanelUI;
    Image DoneIcon;

    public RewardPanelUI GetClearItem;

    public CompainPanelArrange compainPanelArrange;

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
        rewardPanelUI = InformationChapterPanel.Find("Reward Panel").GetComponent<RewardPanelUI>();
        DoneIcon = InformationChapterPanel.Find("Done Icon").GetComponent<Image>();
    }
    public void SetChapterInfomation(Chapter chapter)
    {
        if (this.chapter == null)
            SetAttr();

        this.chapter = chapter;

        gameObject.SetActive(true);

        rewardPanelUI.SetRewardList(this.chapter.reward, this.chapter.StarCount);

        DoneIcon.gameObject.SetActive(chapter.IsDone);
    }

    public void NoneChapter()
    {
        gameObject.SetActive(false);
    }

    public void EnterButton()
    {
        this.chapter.SetMyTeam(TeamManager.Instance.MyTeam());

        PlayerPrefs.SetString("Current Chapter Name", chapter.Name);
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
        compainPanelArrange.gameObject.SetActive(false);
    }
    public void TurnTo_FormationPanel()
    {
        InformationChapterPanel.gameObject.SetActive(false);
        compainPanelArrange.gameObject.SetActive(true);
        compainPanelArrange.CompainArrangeButton();
    }
}
