using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public Character Player;
    Character Ally1;
    Character Ally2;

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

    public void SetStatsPlayer(List<StatsPanel> Stats, List<StatsPanel> Passives)
    {
        this.Player.AttackDamage = Stats[0].GetValue();
        this.Player.HealthPoint = Stats[1].GetValue();
        this.Player.DefensePoint = Stats[2].GetValue();
        this.Player.Speed = Stats[3].GetValue();

        for (int i = 0; i < Passives.Count; i++)
            this.Player.PassiveList[(Equipment.Passive)i + 1] = Passives[i].GetValue();
    }

    public List<Character> MyTeam()
    {
        List<Character> character = new List<Character>();

        if (Ally1 != null)
            character.Add(Ally1);

        if (Ally2 != null)
            character.Add(Ally2);

        character.Add(Player);
        return character;
    }
}
