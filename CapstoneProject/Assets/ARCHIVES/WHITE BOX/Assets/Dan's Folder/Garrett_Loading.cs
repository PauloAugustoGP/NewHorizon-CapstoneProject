using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Garrett_Loading : MonoBehaviour
{
    public bool startPlay;
    public bool isLoading;
    [SerializeField] Text loadingText;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isLoading == false && startPlay == true)
        {
            isLoading = true;

            StartCoroutine(LoadGarrettScene());

            loadingText.text = "Garrett...";
            //SceneManager.LoadSceneAsync("Loading Screen");
        }

        if (isLoading == true)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }

    }

    public void starting()
    {
        startPlay = true;
    }


    IEnumerator LoadGarrettScene()
    {
        Debug.Log("Is Loading");

        AsyncOperation async = SceneManager.LoadSceneAsync("Warehouse_Level");
        Sound_Manager.instance.MusicCaller("LevelMusic", 0.5f);

        while (!async.isDone)
        {
            yield return null;
        }

    }
}
