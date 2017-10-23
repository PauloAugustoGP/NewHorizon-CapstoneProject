using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{
    public AndreController FindController;
    public RectTransform messageSpawnPoint;
    public PlayerMessage spawn;


    public void SpawnMessage(string message)
    {
        PlayerMessage pm = Instantiate(spawn);
        pm.transform.SetParent(messageSpawnPoint.transform);
        pm.text.text = message;
    }
 
}
