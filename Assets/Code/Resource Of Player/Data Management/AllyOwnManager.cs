using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyOwnManager : MonoBehaviour
{
    Dictionary<string, Character> AllAllies = new Dictionary<string, Character>();
    Dictionary<Character.Tier, Dictionary<string, Character>> AllyTiers = new Dictionary<Character.Tier, Dictionary<string, Character>>();
    Dictionary<Character.Role, Dictionary<string, Character>> AllyRoles = new Dictionary<Character.Role, Dictionary<string, Character>>();

    List<Character> charInTeamList = new List<Character>(new Character[3]);
    public static AllyOwnManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        SetAllAlly();
    }

    public void SetAllAlly()
    {
        List<AllySO> ListAllySO = new List<AllySO>(Resources.LoadAll<AllySO>("Character"));
        int characterInTeamCount = 0;
        foreach (AllySO a in ListAllySO)
        {
            //allies in team
            if (a.character.IsInTeam == true && a.character.PositionInTeam > 0)
            {
                charInTeamList[characterInTeamCount] = a.character;
                characterInTeamCount += 1;
            }


            //all allies
            string charName = a.character.Name;
            if(AllAllies.ContainsKey(charName) == false)
                AllAllies[charName] = a.character;


            //sort tier
            Character.Tier charTier = a.character.tier;
            if(AllyTiers.ContainsKey(charTier) == false)
            {
                Dictionary<string, Character> kvps = new Dictionary<string, Character>
                {
                    { charName,a.character }
                };
                AllyTiers.Add(charTier, kvps);
            }
            else
                AllyTiers[charTier].Add(charName,a.character);


            //sort role
            Character.Role charRole = a.character.role;
            if (AllyRoles.ContainsKey(charRole) == false)
            {
                Dictionary<string, Character> kvps = new Dictionary<string, Character>
                {
                    { charName,a.character }
                };
                AllyRoles.Add(charRole, kvps);
            }
            else
                AllyRoles[charRole].Add(charName, a.character);
        }
    }


    public Dictionary<string,Character> GetAllAlly()
    {
        return AllAllies;
    }

    public List<Character> CharTeam()
    {
        return this.charInTeamList;
    }
}
