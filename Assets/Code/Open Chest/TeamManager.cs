using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public Character Player;
    Character Ally1;
    Character Ally2;
    public CharacterSlot AllySlot1;
    public CharacterSlot AllySlot2;

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
    void Start()
    {

    }

    public void SetSkillOfPlayer()
    {

    }
    //call in Stats Panel Manager
    public void SetStatsPlayer(List<StatsPanel> Stats, List<StatsPanel> Passives)
    {
        this.Player.AttackDamage = Stats[0].GetValue();
        this.Player.HealthPoint = Stats[1].GetValue();
        this.Player.DefensePoint = Stats[2].GetValue();
        this.Player.Speed = Stats[3].GetValue();

        for (int i = 0; i < Passives.Count; i++)
            this.Player.PassiveList[(Equipment.Passive)i + 1] = Passives[i].GetValue();
    }

    public bool SetCompainAlly(Character character)
    {
        if(this.Ally1 == null)
        {
            SetStatsAlly1(character);
            return true;
        }

        if(this.Ally2 == null)
        {
            SetStatsAlly2(character);
            return true;
        }

        return false;
    }
    public void RemoveCompainAlly(Character character)
    {
        if(character.Name == this.Ally1.Name && this.Ally1.Name != null)
        {
            RemoveAlly1();
            return;
        }
        if(character.Name == this.Ally2.Name && this.Ally2.Name != null)
        {
            RemoveAlly2();
            return;
        }
    }

    public void SetStatsAlly1(Character character)
    {
        if (this.Ally1 != null)
            RemoveAlly1();

        this.Ally1 = character;
        this.Ally1.IsInTeam = true;
        AllySlot1.SetCharacterInSlot(this.Ally1);
    }
    public void RemoveAlly1()
    {
        this.Ally1.IsInTeam = false;
        this.Ally1 = null;
        AllySlot1.SetCharacterInSlot(this.Ally1);
    }

    public void SetStatsAlly2(Character character)
    {
        if (this.Ally2 != null)
            RemoveAlly2();

        this.Ally2 = character;
        this.Ally2.IsInTeam = true;
        AllySlot2.SetCharacterInSlot(this.Ally2);
    }
    public void RemoveAlly2()
    {
        this.Ally2.IsInTeam = false;
        this.Ally2 = null;
        AllySlot2.SetCharacterInSlot(null);
    }

    public List<Character> MyTeam()
    {
        List<Character> character = new List<Character>();
        Ally1 = AllySlot1.character;
        Ally2 = AllySlot2.character;

        if (Ally1 != null)
            character.Add(Ally1);

        if (Ally2 != null)
            character.Add(Ally2);

        character.Add(Player);
        return character;
    }

    public Character GetAlly1()
    {
        return Ally1;
    }
    public Character GetAlly2()
    {
        return Ally2;
    }
}
