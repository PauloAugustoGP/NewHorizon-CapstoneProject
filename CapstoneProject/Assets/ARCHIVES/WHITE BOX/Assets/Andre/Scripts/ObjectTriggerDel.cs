using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTriggerDel : MonoBehaviour {

    public GameObject TextFirstDiscription;
    public GameObject TextToSetActive;
    public GameObject TextToSetActiveIfTrue;
    public Image imageBlack;
    public Image imageWhite;
    public GameObject headerText;
    public bool firstTextON;

    private void OnTriggerEnter(Collider c)
    {
        imageBlack.gameObject.SetActive(true);
        imageWhite.gameObject.SetActive(true);
        headerText.gameObject.SetActive(true);
        TextFirstDiscription.gameObject.SetActive(true);
        if (firstTextON == false)
        {
            TextFirstDiscription.gameObject.SetActive(false);
        }

        if (c.GetComponent<CharacterBehaviour>().GetHealthRatio() != 100)
        {
            TextFirstDiscription.gameObject.SetActive(false);
            TextToSetActive.gameObject.SetActive(true);
        }
        else
        {
            TextFirstDiscription.gameObject.SetActive(false);
            TextToSetActiveIfTrue.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (firstTextON == true)
        {
            TextFirstDiscription.gameObject.SetActive(false);
        }
    
            TextToSetActive.gameObject.SetActive(false);
            TextFirstDiscription.gameObject.SetActive(true);
            TextToSetActiveIfTrue.gameObject.SetActive(false);
        //if (firstTextON == true)
        //{
        //    imageBlack.gameObject.SetActive(false);
        //    imageWhite.gameObject.SetActive(false);
        //    headerText.gameObject.SetActive(false);
        //}
    
    }
}
