using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

    public bool startPlay;
    public bool isLoading;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (isLoading == false && startPlay == true)
        {
            isLoading = true;

            StartCoroutine(LoadNextScene());

            //SceneManager.LoadSceneAsync("Loading Screen");
        }
        
	}

    public void starting()
    {
        startPlay = true;
    }



    IEnumerator LoadNextScene()
    {
        Debug.Log("Is Loading");
        yield return new WaitForSeconds(3);

        AsyncOperation async = SceneManager.LoadSceneAsync("Test");

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
