using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour
{
    public Text text;
    public float Timer = 30.0f;

    private void Update()
    {

        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetLifeTime(float life)
    {
        Timer = life;
    }

}

