using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Equipment;

public class AllyGachaPanel : MonoBehaviour
{
    public ReceivePanel receivePanel;

    static List<Character> TierCommonCharacter = new List<Character>();
    static List<Character> TierRareCharacter = new List<Character>();
    static List<Character> TierEpicCharacter = new List<Character>();
    static List<Character> TierLegendaryCharacter = new List<Character>();

    Dictionary<Character.Tier, List<Character> > RateInfomation = new Dictionary<Character.Tier, List<Character> >()
    {   { Character.Tier.Common, TierCommonCharacter} ,
        { Character.Tier.Rare,TierRareCharacter},
        { Character.Tier.Epic, TierEpicCharacter},
        { Character.Tier.Legendary, TierLegendaryCharacter},
    };
    List<float> RandomProb = new List<float>() { 70, 90, 99, 100 };
    // 0 - 70 white , 70 - 90 - green , 90 - 99 - blue , 99 - 100 purple

    int times;
    void Start()
    {
        foreach(AllySO c in Resources.LoadAll<AllySO>("Character"))
        {
            RateInfomation[c.character.tier].Add(c.character);
        }
    }



    public void RandomAlly(int times)
    {
        this.times = times;

        string paragraph = "Are you sure about play this gacha? \n You can some stronger ally, but only when you lucky hehe\n Happy Happy Happy!";
        InformManager.Instance.Initialize_QuestionObject("Gacha " + times * 160 + " Diamond", paragraph, AcceptRandom);
    }

    void AcceptRandom()
    {
        if (!ResourceManager.Instance.CheckEnough_Diamond(times * 160))
            return;

        List<Character> getCharList = new List<Character>();
        ResourceManager.Instance.ChangeDiamond(-times * 160);
        for (int i = 0; i < times; i++)
        {
            float r = UnityEngine.Random.Range(0f, 100f);

            int t = RandomTier(r);
            Character.Tier tier = (Character.Tier)t;
            Character getChar = RandomCharacter(RateInfomation[tier]);

            getCharList.Add(getChar.Clone());

            if (getChar.IsOwn)
                getChar.CurrentSharp += 10 * (t + 1);

            getChar.IsOwn = true;

        }

        receivePanel.SetReceiveList(getCharList);
    }

    int RandomTier(float accuracy)
    {
        for (int i = 0; i < RandomProb.Count; i++)
        {
            if (accuracy < RandomProb[i])
            {
                return i;
            }
        }
        return -1;
    }

    Character RandomCharacter(List<Character> characters)
    {
        int index = UnityEngine.Random.Range(0,characters.Count);
        return characters[index];

    }
}
