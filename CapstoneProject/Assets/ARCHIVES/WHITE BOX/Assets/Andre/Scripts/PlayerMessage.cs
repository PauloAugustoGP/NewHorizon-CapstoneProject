using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour
{
    public Text text;
    public float timer = 30.0f;
    public float currentTime;

    private void Start()
    {
        currentTime = timer;
    }
    private void Update()
    {

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            this.gameObject.SetActive(false);
        }
       
    }

    public void SetLifeTime(float time = 10f)
    {
        currentTime = time;
    }

}

