using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChapterListManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ContainerChapter;
    List<ChapterPanelUI> chapterPanelUI = new List<ChapterPanelUI>();
    int showIndex = 0;
    void Start()
    {
        
    }

    public void SetChapterList(List<Chapter> chapter)
    {
        if (chapterPanelUI.Count == 0)
            SetAttr();

        for (int i = 0; i < 10; i++)
            if (i < chapter.Count)
                chapterPanelUI[i].SetChapterInfomation(chapter[i]);
            else chapterPanelUI[i].NoneChapter();
    }

    public void SetAttr()
    {
        for (int i = 0; i < 10; i++)
            if (ContainerChapter.transform.GetChild(i) != null)
            {
                ChapterPanelUI c = ContainerChapter.transform.GetChild(i).GetComponent<ChapterPanelUI>();
                chapterPanelUI.Add(c);
                //if (i != showIndex)
                //    c.gameObject.SetActive(false);
            }
    }

    public void ShowWhenMoveScroll()
    {
        //if(showIndex)
        //showIndex
    }

    public void ClickOnBackground()
    {
        gameObject.SetActive(false);
    }
}
