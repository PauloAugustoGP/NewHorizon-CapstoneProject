using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Text))]

public class TextManager : MonoBehaviour {

    private Typing typer;
    private Animator menuAnim;
    private bool menuOn = false;

    private void Awake()
    {
        typer = GetComponentInChildren<Typing>();
        menuAnim = GetComponent<Animator>();
    }

    public void BeginTheMenu()
    {
        if(!menuOn)
        {
            menuAnim.SetTrigger("FadeIn");
            typer.StartCoroutine("TypeIn");
            menuOn = true;
        }
        else
        {
            menuAnim.SetTrigger("FadeOut");
            typer.StartCoroutine("TypeOff");
            menuOn = false;
        }
    }
}
