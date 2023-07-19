using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompainPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public CompainSlot Ally1Slot;
    Character Ally1;
    public CompainSlot Ally2Slot;
    Character Ally2;

    public DetailAllyPanel detailAllyPanel;
    Character Ally3;

    string Change;

    public TextMeshProUGUI CompainText;
    public Image CompainIcon;
    void Start()
    {
        
    }

    public void ChooseAllyToChange()
    {
        gameObject.SetActive(true);

        Ally1 = TeamManager.Instance.GetAlly1();
        Ally1Slot.SetCharacterInSlot(Ally1);

        Ally2 = TeamManager.Instance.GetAlly2();
        Ally2Slot.SetCharacterInSlot(Ally2);

        Ally3 = detailAllyPanel.characterData;

        Change = "";
    }


    public void CompainButton()
    {
        if (detailAllyPanel.characterData.IsInTeam)
        {
            Ally3 = detailAllyPanel.characterData;
            TeamManager.Instance.RemoveCompainAlly(Ally3);
            CompainText.SetText("Compain");
            CompainIcon.gameObject.SetActive(false);
            return;
        }

        //if true - just have set ally in empty slot
        if (TeamManager.Instance.SetCompainAlly(detailAllyPanel.characterData))
        {
            CompainText.SetText("Uncompain");
            CompainIcon.gameObject.SetActive(true);
            return;
        }

        ChooseAllyToChange();
    }

    public void BackgroundClick()
    {
        gameObject.SetActive(false);
    }

    public void OkButton()
    {
        if (Change == "1")
            TeamManager.Instance.SetStatsAlly1(Ally3);

        if (Change == "2")
            TeamManager.Instance.SetStatsAlly2(Ally3);


        if(Change == "")
        {
            CompainText.SetText("Compain");
            CompainIcon.gameObject.SetActive(false);
        }
        else
        {
            CompainText.SetText("Uncompain");
            CompainIcon.gameObject.SetActive(true);
        }
        Ally3 = null;
        gameObject.SetActive(false);
    }

    public void Ally1Click()
    {
        Ally1Slot.SetCharacterInSlot(Ally3);
        Ally2Slot.SetCharacterInSlot(Ally2);
        Change = "1";
    }

    public void Ally2Click()
    {
        Ally2Slot.SetCharacterInSlot(Ally3);
        Ally1Slot.SetCharacterInSlot(Ally1);

        Change = "2";
    }
}
