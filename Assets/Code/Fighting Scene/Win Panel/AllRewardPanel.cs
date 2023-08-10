using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllRewardPanel : MonoBehaviour
{
    public List<AllyReward> allyGetRewardList;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Chapter currentChapter)
    {
        int c = FightManager.Instance.PlayerTeam.Count;
        for (int i = 0; i < 4; i++)
            if(i < c)
                allyGetRewardList[i].Initialize(currentChapter.MyTeam[i].Clone(), currentChapter.ExpForCharacter);
            else
                allyGetRewardList[i].Initialize(null, 0);
    }
}
