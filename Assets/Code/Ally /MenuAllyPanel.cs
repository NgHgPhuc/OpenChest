using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAllyPanel : MonoBehaviour
{
    public GameObject InfoPanel;
    public GameObject SkillPanel;
    public GameObject TranscendPanel;
    GameObject CurrentPanel;
    public Transform ChosenEffect;
    void Start()
    {
        CurrentPanel = InfoPanel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveChosenEffect(Vector3 posObject)
    {
        StopAllCoroutines();
        Vector3 pos = new Vector3(posObject.x, ChosenEffect.position.y, 1);
        float distance = posObject.x - ChosenEffect.position.x;
        IEnumerator animation = Move(60, distance / 60f);
        StartCoroutine(animation);
    }

    IEnumerator Move(int Times,float distance)
    {
        int i = 0;
        while(i<Times)
        {
            Vector3 movePos = new Vector3(ChosenEffect.position.x+distance, ChosenEffect.position.y, 1);

            ChosenEffect.position = movePos;
            i++;
            yield return new WaitForSeconds(0.0025f);
        }

        yield break;
    }

    public void ButtonInfo(Transform Button)
    {
        if(CurrentPanel == null)
            CurrentPanel = InfoPanel;

        CurrentPanel.SetActive(false);
        InfoPanel.SetActive(true);
        MoveChosenEffect(Button.position);
        CurrentPanel = InfoPanel;
    }

    public void ButtonSkill(Transform Button)
    {
        if (CurrentPanel == null)
            return;

        CurrentPanel.SetActive(false);
        SkillPanel.SetActive(true);
        MoveChosenEffect(Button.position);
        CurrentPanel = SkillPanel;
    }

    public void ButtonTranscend(Transform Button)
    {
        if (CurrentPanel == null)
            return;

        CurrentPanel.SetActive(false);
        TranscendPanel.SetActive(true);
        MoveChosenEffect(Button.position);
        CurrentPanel = TranscendPanel;
    }
}
