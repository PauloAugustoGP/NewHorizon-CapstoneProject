using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WorldSpaceText : MonoBehaviour {
    //Attach this worldSpaceText Prefab to any object you want to have a text above when you enter the trigger.
    //Drag the game object into "theGameObjecttheTextisAttachedTo" to make the trigger work.
    //Drag in the player to "Target to rotate TO", to make the text rotate in world space to the player.
    public GameObject worldSpaceText;
    public GameObject theGameObjecttheTextisAttachedTo;

    public GameObject targetToRotateTO;

    void Update()
    {
        if (!targetToRotateTO)
        {
            return;
        }
        //might offset trigger
        var theCamOrPlayer = transform.position;
        var theObject = targetToRotateTO.transform.position;
        transform.rotation = Quaternion.LookRotation(theCamOrPlayer - theObject, Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        worldSpaceText.gameObject.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(theGameObjecttheTextisAttachedTo);
        //theGameObjecttheTextisAttachedTo.GetComponentInChildren <MeshRenderer>(false);
    }
}
