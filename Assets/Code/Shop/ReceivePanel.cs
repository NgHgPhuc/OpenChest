using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivePanel : MonoBehaviour
{
    public Transform ReceiveList;
    List<ReceiveObject> receiveObjectList = new List<ReceiveObject>();
    Animator animator;
    void Start()
    {
        
    }

    public void SetReceiveList(List<Character> characterList)
    {
        if (receiveObjectList.Count == 0)
            for (int i = 0; i < ReceiveList.childCount; i++)
                for(int j = 0; j< ReceiveList.GetChild(i).childCount; j++)
                    receiveObjectList.Add(ReceiveList.GetChild(i).GetChild(j).GetComponent<ReceiveObject>());

        gameObject.SetActive(true);

        for (int i = 0; i < receiveObjectList.Count; i++)
            if (i < characterList.Count)
                receiveObjectList[i].SetReceiveCharacter(characterList[i]);
            else
                receiveObjectList[i].NoneReceive();

        if (animator == null)
            animator = GetComponent<Animator>();

        //animator.Play("Receive");
    }

    public void SetReceiveList_Skill(List<BaseSkill> skillList)
    {
        SetAttr();

        gameObject.SetActive(true);

        for (int i = 0; i < receiveObjectList.Count; i++)
            if (i < skillList.Count)
                receiveObjectList[i].SetReceiveSkill(skillList[i]);
            else
                receiveObjectList[i].NoneReceive();

        if (animator == null)
            animator = GetComponent<Animator>();

        //animator.Play("Receive");
    }

    void SetAttr()
    {
        if (receiveObjectList.Count == 0)
            for (int i = 0; i < ReceiveList.childCount; i++)
                for (int j = 0; j < ReceiveList.GetChild(i).childCount; j++)
                    receiveObjectList.Add(ReceiveList.GetChild(i).GetChild(j).GetComponent<ReceiveObject>());
    }
    public void ClickBackground()
    {
        gameObject.SetActive(false);
    }
}
