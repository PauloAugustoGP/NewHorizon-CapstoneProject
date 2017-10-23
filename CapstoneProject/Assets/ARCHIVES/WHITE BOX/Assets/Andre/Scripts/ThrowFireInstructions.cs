using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFireInstructions : MonoBehaviour {

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
                    pm.SpawnMessage("Press the space bar and hold to charge fire ball, let go to throw.");
                }
            }
        }
    }
    private void OnTriggerExit(Collider c)
    {
        Destroy(gameObject);
    }
}
