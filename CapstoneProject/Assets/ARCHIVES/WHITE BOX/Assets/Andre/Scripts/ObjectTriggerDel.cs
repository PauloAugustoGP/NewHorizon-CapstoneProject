using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTriggerDel : MonoBehaviour {

    public GameObject TextFirstDiscription;
    public GameObject TextToSetActive;
    public GameObject TextToSetActiveIfTrue;

    private void OnTriggerEnter(Collider c)
    {
        //change get component to get the bool that is or is not true for what ever object you might want to implement.
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
        TextToSetActive.gameObject.SetActive(false);
        TextFirstDiscription.gameObject.SetActive(true);
        TextToSetActiveIfTrue.gameObject.SetActive(false);
        //if (other.GetComponent<WorldSpaceText>().theGameObject == false)
        //{
        //    Destroy(gameObject);
        //}
    }
}
