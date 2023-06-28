using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InformManager : MonoBehaviour
{
    public InformObject informObject;
    public GameObject Background;
    public Animator animator;

    public GameObject floatingObject;
    public Transform floatingPos;

    public static InformManager Instance { get; private set; }
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

    void TurnOffObject()
    {
        informObject.gameObject.SetActive(false);
        Background.gameObject.SetActive(false);
    }

    public void Initialize_InformObject(string Title, string Paragraph, UnityAction ConfirmEvent)
    {
        informObject.gameObject.SetActive(true);
        Background.gameObject.SetActive(true);

        animator.Play("Inform Object");

        this.informObject.Initialize_InformObject(Title, Paragraph, ConfirmEvent + TurnOffObject);
    }

    public void Initialize_QuestionObject(string Title, string Paragraph, UnityAction ConfirmEvent)
    {
        informObject.gameObject.SetActive(true);
        Background.gameObject.SetActive(true);

        animator.Play("Inform Object");

        this.informObject.Initialize_QuestionObject(Title, Paragraph, ConfirmEvent + TurnOffObject, TurnOffObject);
    }

    public void Initialize_FloatingInform(string mes)
    {
        GameObject i = Instantiate(floatingObject, floatingPos.position, floatingPos.rotation, transform);
        InformFloating informFloating = i.GetComponentInChildren<InformFloating>();
        informFloating.Initialize_InformFloating(mes);
    }
}
