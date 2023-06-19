using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapObject : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public ChapterListManager chapterListManager;
    public List<Chapter> chapter = new List<Chapter>();
    void Start()
    {
        int ChapterCount = Resources.LoadAll<Chapter>("Chapter").Length;
        for (int i = 1; i <= ChapterCount; i++)
            chapter.Add(Resources.Load<Chapter>("Chapter/Chapter " + i));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        chapterListManager.gameObject.SetActive(true);
        chapterListManager.SetChapterList(chapter);
    }
}
