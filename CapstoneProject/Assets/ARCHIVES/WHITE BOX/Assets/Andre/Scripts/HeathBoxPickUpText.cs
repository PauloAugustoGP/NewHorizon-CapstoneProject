using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathBoxPickUpText : MonoBehaviour {
    public RectTransform PickUpHealthSpawnPoint;
    public PlayerMessage spawn;
    private void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.tag == "Player")
        {
                    SpawnMessage("You picked up a med kit.");
        }
    }
    private void OnTriggerExit(Collider c)
    {
        Destroy(gameObject);
    }
    public void SpawnMessage(string message)
    {
        PlayerMessage pm = Instantiate(spawn);
        pm.transform.SetParent(PickUpHealthSpawnPoint.transform);
        pm.text.text = message;
        
    }
}
