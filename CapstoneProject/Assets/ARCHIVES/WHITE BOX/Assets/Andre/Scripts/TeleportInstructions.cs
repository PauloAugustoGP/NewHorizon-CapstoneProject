using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportInstructions : MonoBehaviour {

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
                    pm.SpawnMessage("Press and hold down tab, point with your mouse to the area you want to teleport to, then let go of tab.");
                }
            }
        }
    }
    private void OnTriggerExit(Collider c)
    {
        Destroy(gameObject);
    }
}
