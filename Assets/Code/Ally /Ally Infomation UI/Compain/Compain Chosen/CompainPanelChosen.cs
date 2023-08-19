using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class CompainPanelChosen : MonoBehaviour
{
    public List<CompainSlot> compainSlotUIs = new List<CompainSlot>();

    public DetailAllyPanel detailAllyPanel;

    bool IsChosing;

    void Start()
    {

    }

    public void BackgroundClick()
    {
        gameObject.SetActive(false);
    }

    public void CompainChosenButton()
    {
        print("Compain");
        if (detailAllyPanel.character.IsInTeam == false)
            ChooseAllyToChange();
        else
            RemoveAllyCompained();
    }

    public void RemoveAllyCompained()
    {
        print("Remove");
        Character character = detailAllyPanel.character;
        int index = character.PositionInTeam;
        TeamManager.Instance.RemoveAlly(index);
        detailAllyPanel.allyInfomationUI.Set_IsCompain(character);
    }

    public void ChooseAllyToChange()
    {
        gameObject.SetActive(true);
        print("Chosen");
        for (int i = 0; i < 4; i++)
        {
            Character c = TeamManager.Instance.GetAllyInTeam()[i];
            if (c != null)
                compainSlotUIs[i].SetCharacterInSlot(c);
            else
                compainSlotUIs[i].DontHaveCharacterInSlot();
        }
        IsChosing = true;
    }

    public void AllyCompainClick(int index)
    {
        if (IsChosing == false)
            return;

        if (!TeamManager.Instance.CanRemoveThisSlot(index))
            return;

        Character AllyChosing = detailAllyPanel.character;

        TeamManager.Instance.SetCompainAlly(AllyChosing, index);
        compainSlotUIs[index - 1].SetCharacterInSlot(AllyChosing);

        IsChosing = false;

        InformManager.Instance.Initialize_FloatingInform("Compain successfully ally!");
        detailAllyPanel.allyInfomationUI.Set_IsCompain(AllyChosing);
    }
}
