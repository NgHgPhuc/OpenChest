using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ChapterListManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI chapterNameText;
    public ChapterPanelUI chapterPanelUI;
    int showIndex = 0;
    void Start()
    {
        
    }

    public void SetChapter(Chapter chapter)
    {
        chapterPanelUI.SetChapterInfomation(chapter);
        chapterNameText.SetText(chapter.Name);
    }


    public void ClickOnBackground()
    {
        gameObject.SetActive(false);
    }
}
