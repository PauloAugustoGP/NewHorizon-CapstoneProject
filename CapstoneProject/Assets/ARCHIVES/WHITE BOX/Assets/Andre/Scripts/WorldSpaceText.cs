using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WorldSpaceText : MonoBehaviour {
    //Attach any object you want to have a text above when you enter the trigger to this worldSpaceText Prefab.
    //Drag the game object into "theGameObjecttheTextisAttachedTo" to make the trigger work.
    //Drag in the camera or player to "Target to rotate TO", to make the text rotate in world space to the camera or player.
    public GameObject worldSpaceText;
    public GameObject theGameObjecttheTextisAttachedTo;
    [SerializeField] BoxCollider TheGameObjectBoxCollider;
    public GameObject targetToRotateTO;
    public bool theGameObject = true;
    public GameObject triggerToDestroy;
    

    void Update()
    {
        if (theGameObjecttheTextisAttachedTo)
        {
            if (!targetToRotateTO)
            {
                return;
            }
            var theCamOrPlayer = transform.position;
            var theObject = targetToRotateTO.transform.position;
            transform.rotation = Quaternion.LookRotation(theCamOrPlayer - theObject, Vector3.up);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {  
            TheGameObjectBoxCollider.enabled = false;
            
    }

    private void OnTriggerEnter(Collider other)
    {
        worldSpaceText.gameObject.SetActive(true);
        if (other.GetComponent<CharacterBehaviour>().atFullHealth == false)
        {
            theGameObject = false;
            
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        worldSpaceText.gameObject.SetActive(false);
       
            if (theGameObject == false)
            {
                Destroy(triggerToDestroy);
            }
        
    }
}
