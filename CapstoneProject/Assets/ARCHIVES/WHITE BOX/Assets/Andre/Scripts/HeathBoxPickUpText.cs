using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathBoxPickUpText : MonoBehaviour {

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {

            GameObject gm = GameObject.FindGameObjectWithTag("MainCamera");
            if (gm)
            {
                PlayerUI pm = gm.GetComponent<PlayerUI>();
                if (pm)
                {
                    pm.SpawnMessage("You picked up a med kit.");
                }
            }
        }
    }
    private void OnTriggerExit(Collider c)
    {
        Destroy(gameObject);
    }
}
