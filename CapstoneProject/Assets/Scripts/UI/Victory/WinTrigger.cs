using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
	void Update ()
    {
        transform.Rotate(0.5f, 0.5f, 0.5f);
	}

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            Sound_Manager.instance.MusicCaller("WinningMusic");
            Scene_Manager.instance.GoTo_LEVEL("Winning");
        }
    }
}
