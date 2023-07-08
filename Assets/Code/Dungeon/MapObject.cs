using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MapObject : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    Chapter chapter;

    public int Index;
    public ChapterListManager chapterListManager;
    public StarListPanel starListPanel;
    public TextMeshProUGUI ChapterName;
    void Start()
    {
        this.chapter = Resources.Load<Chapter>("Chapter/Chapter " + Index);
        ChapterName.SetText(this.chapter.Name);
        starListPanel.SetStarCount(this.chapter.StarCount);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        chapterListManager.gameObject.SetActive(true);
        chapterListManager.SetChapter(this.chapter);
    }
}
