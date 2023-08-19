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

    public void Initialize(List<Character> characterList,int ExpForCharacter)
    {
        for (int i = 0; i < 3; i++)
            if (characterList[i] != null)//cloned before reference
                allyGetRewardList[i].Initialize(characterList[i], ExpForCharacter);
            else
                allyGetRewardList[i].Initialize(null, 0);
    }
}
