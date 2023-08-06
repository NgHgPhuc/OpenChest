using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
        foreach (Character c in AllyOwnManager.Instance.CharTeam())
            if (c != null)
                AllyInTeam[c.PositionInTeam - 1] = c;

        Character player = PlayerManager.Instance.GetPlayer();
        AllyInTeam[player.PositionInTeam - 1] = player;

        for (int i = 0; i < 3; i++)
            AllySlotUIs[i].SetCharacterInSlot(AllyOwnManager.Instance.CharTeam()[i]);
    }

    public bool CanRemoveThisSlot(int slotIndex)
    {
        if (AllyInTeam[slotIndex - 1] == null)
            return true;

        if (AllyInTeam[slotIndex - 1].Name == "Player")
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
        //DataManager.Instance.SaveData(character.Name, character.ToStringData());

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
        //print(AllyInTeam[index1 - 1].Name + "/" + AllyInTeam[index1 - 1].PositionInTeam);
        //print(AllyInTeam[index2 - 1].Name + "/" + AllyInTeam[index2 - 1].PositionInTeam);
    }
    public List<Character> MyTeam()
    {
        List<Character> characters = new List<Character>();
        return characters;
    }
    public List<Character> GetAllyInTeam()
    {
        return this.AllyInTeam;
    }


    //public bool SetCompainAlly(Character character)
    //{
    //    if(this.Ally1 == null)
    //    {
    //        SetStatsAlly1(character);
    //        return true;
    //    }

    //    if(this.Ally2 == null)
    //    {
    //        SetStatsAlly2(character);
    //        return true;
    //    }

    //    return false;
    //}
    //public void RemoveCompainAlly(Character character)
    //{
    //    if(character.Name == this.Ally1.Name && this.Ally1.Name != null)
    //    {
    //        RemoveAlly1();
    //        return;
    //    }
    //    if(character.Name == this.Ally2.Name && this.Ally2.Name != null)
    //    {
    //        RemoveAlly2();
    //        return;
    //    }
    //}

    //public void SetStatsAlly1(Character character)
    //{
    //    if (this.Ally1 != null)
    //        RemoveAlly1();

    //    this.Ally1 = character;
    //    this.Ally1.IsInTeam = true;
    //    //DataManager.Instance.SaveData(character.Name, character.ToStringData());
    //    AllySlot1.SetCharacterInSlot(this.Ally1);
    //}
    //public void RemoveAlly1()
    //{
    //    this.Ally1.IsInTeam = false;
    //    this.Ally1 = null;
    //    AllySlot1.DontHaveCharacterInSlot();
    //}

    //public void SetStatsAlly2(Character character)
    //{
    //    if (this.Ally2 != null)
    //        RemoveAlly2();

    //    this.Ally2 = character;
    //    this.Ally2.IsInTeam = true;
    //    //DataManager.Instance.SaveData(character.Name, character.ToStringData());
    //    AllySlot2.SetCharacterInSlot(this.Ally2);
    //}
    //public void RemoveAlly2()
    //{
    //    this.Ally2.IsInTeam = false;
    //    this.Ally2 = null;
    //    AllySlot2.DontHaveCharacterInSlot();
    //}

    //public List<Character> MyTeam()
    //{
    //    List<Character> characters = new List<Character>();
    //    Ally1 = AllySlot1.character;
    //    Ally2 = AllySlot2.character;

    //    if (Ally1 != null)
    //        characters.Add(Ally1);

    //    if (Ally2 != null)
    //        characters.Add(Ally2);

    //    characters.Add(PlayerManager.Instance.GetPlayer());
    //    return characters;
    //}

    //public Character GetAlly1()
    //{
    //    return Ally1;
    //}
    //public Character GetAlly2()
    //{
    //    return Ally2;
    //}


    //public bool SetCompainAlly(Character character)
    //{
    //    if(this.Ally1 == null)
    //    {
    //        SetStatsAlly1(character);
    //        return true;
    //    }

    //    if(this.Ally2 == null)
    //    {
    //        SetStatsAlly2(character);
    //        return true;
    //    }

    //    return false;
    //}
    //public void RemoveCompainAlly(Character character)
    //{
    //    if(character.Name == this.Ally1.Name && this.Ally1.Name != null)
    //    {
    //        RemoveAlly1();
    //        return;
    //    }
    //    if(character.Name == this.Ally2.Name && this.Ally2.Name != null)
    //    {
    //        RemoveAlly2();
    //        return;
    //    }
    //}

    //public void SetStatsAlly1(Character character)
    //{
    //    if (this.Ally1 != null)
    //        RemoveAlly1();

    //    this.Ally1 = character;
    //    this.Ally1.IsInTeam = true;
    //    //DataManager.Instance.SaveData(character.Name, character.ToStringData());
    //    AllySlot1.SetCharacterInSlot(this.Ally1);
    //}
    //public void RemoveAlly1()
    //{
    //    this.Ally1.IsInTeam = false;
    //    this.Ally1 = null;
    //    AllySlot1.DontHaveCharacterInSlot();
    //}

    //public void SetStatsAlly2(Character character)
    //{
    //    if (this.Ally2 != null)
    //        RemoveAlly2();

    //    this.Ally2 = character;
    //    this.Ally2.IsInTeam = true;
    //    //DataManager.Instance.SaveData(character.Name, character.ToStringData());
    //    AllySlot2.SetCharacterInSlot(this.Ally2);
    //}
    //public void RemoveAlly2()
    //{
    //    this.Ally2.IsInTeam = false;
    //    this.Ally2 = null;
    //    AllySlot2.DontHaveCharacterInSlot();
    //}

    //public List<Character> MyTeam()
    //{
    //    List<Character> characters = new List<Character>();
    //    Ally1 = AllySlot1.character;
    //    Ally2 = AllySlot2.character;

    //    if (Ally1 != null)
    //        characters.Add(Ally1);

    //    if (Ally2 != null)
    //        characters.Add(Ally2);

    //    characters.Add(PlayerManager.Instance.GetPlayer());
    //    return characters;
    //}

    //public Character GetAlly1()
    //{
    //    return Ally1;
    //}
    //public Character GetAlly2()
    //{
    //    return Ally2;
    //}
}
