using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTriggerBox : MonoBehaviour {

    public string textToTrigger;
    public GameObject bottomText;
    private Text uiText;
    public float textLifeTime = 10;

    private void Start()
    {
        uiText = bottomText.GetComponentInChildren<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        bottomText.GetComponent<PlayerMessage>().SetLifeTime(textLifeTime);
        bottomText.SetActive(true);

        uiText.text = textToTrigger;
        
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(this.gameObject);
    }
}
