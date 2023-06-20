using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WardPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DungeonPanel;
    public GameObject OpenChestPanel;
    GameObject CurrentGameObject;
    void Start()
    {
        CurrentGameObject = OpenChestPanel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DungeonWarp()
    {
        SetCurrentGameObject(DungeonPanel);
    }

    public void OpenChestWarp()
    {
        SetCurrentGameObject(OpenChestPanel);
    }

    void SetCurrentGameObject(GameObject g)
    {
        if (CurrentGameObject == g)
            return;

        if (CurrentGameObject != null)
            CurrentGameObject.SetActive(false);

        CurrentGameObject = g;
        CurrentGameObject.SetActive(true);
    }
}
