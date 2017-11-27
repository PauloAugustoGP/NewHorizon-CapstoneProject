using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(0.5f, 0.5f, 0.5f);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Sound_Manager.instance.MusicCaller("WinningMusic");
            Scene_Manager.instance.GoTo_LEVEL("Winning");
        }
    }
}
