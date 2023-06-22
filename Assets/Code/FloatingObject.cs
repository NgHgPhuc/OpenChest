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

    public void Iniatialize(string text,Color color)
    {
        image = transform.Find("Image").GetComponent<Image>();
        image.gameObject.SetActive(false);
        this.text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();

        animator.Play("Floating On");
        this.text.SetText(text);
        this.text.color = color;
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }
}
