using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.TextCore.Text;

public class TeamManager : MonoBehaviour
{
    List<Character> AllyInTeam = new List<Character>(new Character[4]);
    public List<CharacterSlot> AllySlotUIs; // to show character in ui openchest

    public static TeamManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }
    private void Start()
    {
        InitializeTeam();
    }
    public void InitializeTeam()
    {
        for (int i = 0; i < 4; i++)
        {
            if(i == 3)
            {
                Character player = PlayerManager.Instance.GetPlayer();
                AllyInTeam[player.PositionInTeam - 1] = player;
                break;
            }

            Character c = AllyOwnManager.Instance.CharTeam()[i];
            AllySlotUIs[i].SetCharacterInSlot(c);
            if (c != null)
                AllyInTeam[c.PositionInTeam - 1] = c;//null dont have position
        }
    }

    public bool CanRemoveThisSlot(int slotIndex)
    {
        if (AllyInTeam[slotIndex - 1] == null)
            return true;

        if (AllyInTeam[slotIndex - 1].Name == "Player Fighting")
            return false;

        return true;
    }
    public void SetCompainAlly(Character character,int slotIndex)
    {
        if(!CanRemoveThisSlot(slotIndex))
        {
            InformManager.Instance.Initialize_FloatingInform("dont try to replace player with another ally!");
            return;
        }

        if (!CanRemoveThisSlot(slotIndex))
            return;

        RemoveAlly(slotIndex);

        AllyInTeam[slotIndex - 1] = character;
        AllyInTeam[slotIndex - 1].IsInTeam = true;
        AllyInTeam[slotIndex - 1].PositionInTeam = slotIndex;
        DataManager.Instance.SaveData(character.Name, character.ToStringData());

        for (int i = 0; i < 3; i++)
            AllySlotUIs[i].SetCharacterInSlot(AllyOwnManager.Instance.CharTeam()[i]);
    }

    public void RemoveAlly(int slotIndex)
    {
        if (!CanRemoveThisSlot(slotIndex))
            return;
        //if null - just replace, not remove
        if (AllyInTeam[slotIndex - 1] == null)
            return;

        AllyInTeam[slotIndex - 1].IsInTeam = false;
        AllyInTeam[slotIndex - 1].PositionInTeam = 0;
        DataManager.Instance.SaveData(AllyInTeam[slotIndex - 1].Name, AllyInTeam[slotIndex - 1].ToStringData());
        AllyInTeam[slotIndex - 1] = null;

        for (int i = 0; i < 3; i++)
            AllySlotUIs[i].SetCharacterInSlot(AllyOwnManager.Instance.CharTeam()[i]);
    }

    public void Swap2Allies(int index1,int index2)
    {
        Character char1 = AllyInTeam[index1 - 1];
        if(char1 != null)
            char1.PositionInTeam = index2;

        Character char2 = AllyInTeam[index2 - 1];
        if (char2 != null)
            char2.PositionInTeam = index1;

        AllyInTeam[index1 - 1] = char2;
        AllyInTeam[index2 - 1] = char1;

        //print(AllyInTeam[index1 - 1] + "-" + index1 + "-->" + AllyInTeam[index2 - 1] + "-" + index2);
    }
    public List<Character> MyTeam()
    {
        return AllyInTeam;
    }
    public List<Character> GetAllyInTeam()
    {
        return this.AllyInTeam;
    }
}
