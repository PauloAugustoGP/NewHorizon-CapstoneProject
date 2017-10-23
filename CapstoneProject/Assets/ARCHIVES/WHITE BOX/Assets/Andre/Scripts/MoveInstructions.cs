using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInstructions : MonoBehaviour {

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
                    pm.SpawnMessage("Press W to move forward A to move left D to move right and S to move back.");
                }
            }
        }
    }
    private void OnTriggerExit(Collider c)
    {
        Destroy(gameObject);
    }
}

