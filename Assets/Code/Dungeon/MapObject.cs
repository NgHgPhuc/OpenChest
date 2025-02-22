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
    CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        this.chapter = Resources.Load<Chapter>("Chapter/Chapter " + Index);
        ChapterName.SetText(this.chapter.Name);
        starListPanel.SetStarCount(this.chapter.StarCount,3,3);

        if (this.chapter.IsOpen == false)
            canvasGroup.alpha = 0.7f;
        else
            canvasGroup.alpha = 1f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.chapter.IsOpen == false)
        {
            InformManager.Instance.Initialize_FloatingInform("This chapter isn't open");
        }
        else
        {
            chapterListManager.gameObject.SetActive(true);
            chapterListManager.SetChapter(this.chapter);
        }
    }
}
