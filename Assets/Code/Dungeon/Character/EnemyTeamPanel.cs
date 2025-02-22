using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeamPanel : MonoBehaviour
{
    // Start is called before the first frame update
    List<CharacterUI> characterUIs = new List<CharacterUI>();
    //List<Character> characterDatas = new List<Character>();
    void Start()
    {
        for (int i = 1; i < 4; i++)
            characterUIs.Add(transform.Find("Character Object Panel " + i).GetComponent<CharacterUI>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCharacterData(List<Character> _characterDatas)
    {
        if(characterUIs.Count == 0)
            for (int i = 1; i < 4; i++)
                characterUIs.Add(transform.Find("Character Object Panel " + i).GetComponent<CharacterUI>());

        //this.characterDatas.Clear();
        //this.characterDatas = new List<Character>(_characterDatas);

        SetCharacterUI(_characterDatas);
    }

    public void SetCharacterUI(List<Character> _characterDatas)
    {
        for (int i = 0; i < 3; i++)
            if (i < _characterDatas.Count && characterUIs[i] != null)
                characterUIs[i].SetCharacterUI(_characterDatas[i]);
            else characterUIs[i].NoneData();
    }


}
