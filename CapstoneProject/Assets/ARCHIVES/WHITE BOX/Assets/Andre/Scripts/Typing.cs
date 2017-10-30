using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Text))]

public class Typing : MonoBehaviour {
    public string msgs = "Replace";
    private Text textComponent;
    public float starterDeley = 2f;
    public float typerDeley = 0.01f;
    public AudioClip tap;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    public IEnumerator TypeIn()
    {
        yield return new WaitForSeconds(starterDeley);
        
        for (int i = 0; i < msgs.Length; i++)
        {
            textComponent.text = msgs.Substring(0, i);
            yield return new WaitForSeconds(typerDeley);
        }
    }

    public IEnumerator TypeOff()
    {
        for(int i = msgs.Length; i >=0; i--)
        {
            textComponent.text = msgs.Substring(0, i);
            yield return new WaitForSeconds(typerDeley);
        }
    }
}
