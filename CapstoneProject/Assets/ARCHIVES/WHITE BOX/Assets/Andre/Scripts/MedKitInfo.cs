using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKitInfo : MonoBehaviour {

    public GameObject HealthPackCanvas;
    public PlayerMessage spawn;
    public GameObject textDel;

    private void OnTriggerEnter(Collider c)
    {
        textDel.gameObject.SetActive(true);
        if (c.gameObject.tag == "Player")
        {
            SpawnMessage("You picked up a med kit.");
        }
    }
    private void OnTriggerExit(Collider c)
    {
        HealthPackCanvas.gameObject.SetActive(true);
        //Destroy(gameObject);
    }
    public void SpawnMessage(string message)
    {
        PlayerMessage pm = Instantiate(spawn);
        pm.text.text = message;

    }
}
