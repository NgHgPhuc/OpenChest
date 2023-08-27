using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingObject : MonoBehaviour
{
    Image image;
    TextMeshProUGUI text;
    Animator animator;
    void Start()
    { 

    }

    public void Iniatialize(string text,Color color,string animation = "Floating On")
    {
        image = transform.Find("Image").GetComponent<Image>();
        image.gameObject.SetActive(false);
        this.text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();

        animator.Play(animation);
        this.text.SetText(text);
        this.text.color = color;
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }

    public void SetSize(float Width,float Height)
    {
        RectTransform r = (RectTransform)this.transform;
        r.sizeDelta = new Vector2(Width, Height);
    }
    public Vector3 GetNextPos()
    {
        RectTransform r = (RectTransform)this.transform;
        return new Vector3(r.rect.center.x, r.rect.center.y, 1);
    }

    public void SetSpeed(int speed)
    {
        animator.speed = speed;
    }
}
