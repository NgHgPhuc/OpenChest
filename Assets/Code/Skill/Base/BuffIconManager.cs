using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffIconManager : MonoBehaviour
{
    Dictionary<Buff.Type, Sprite> icons = new Dictionary<Buff.Type, Sprite>(); 
    public static BuffIconManager Instance { get; private set; }
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

    public Sprite GetIcon(Buff.Type buffType)
    {
        if (icons.ContainsKey(buffType))
            return icons[buffType];

        Texture2D texture = Resources.Load<Texture2D>("Buff/"+ buffType.ToString());
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        icons.Add(buffType, sprite);

        return sprite;
    }
}
