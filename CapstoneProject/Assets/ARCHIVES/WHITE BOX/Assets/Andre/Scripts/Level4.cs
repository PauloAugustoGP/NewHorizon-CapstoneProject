﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level4 : MonoBehaviour
{

    public SpawnRandom spawn;
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            spawn.SpawnEnemies();
            GameObject gm = GameObject.FindGameObjectWithTag("MainCamera");
            if (gm)
            {
                PlayerUI pm = gm.GetComponent<PlayerUI>();
                if (pm)
                {
                    pm.SpawnMessage("Pass all the guards and Exit the building!");
                }
            }
        }
    }
}