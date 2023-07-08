using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChapterListManager : MonoBehaviour
{
    // Start is called before the first frame update
    public ChapterPanelUI chapterPanelUI;
    int showIndex = 0;
    void Start()
    {
        
    }

    public void SetChapter(Chapter chapter)
    {
        chapterPanelUI.SetChapterInfomation(chapter);
    }


    public void ClickOnBackground()
    {
        gameObject.SetActive(false);
    }
}
