using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    Rigidbody rb;
    charControlScript playerScript;

    public float shiftRate;
    float timeSinceLastShift = 0.0f;

    bool isCooled;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerScript = GetComponent<charControlScript>();

        shiftRate = 2.0f;

        isCooled = true;

        

    }

    void Update()
    {
        /*if (Time.time > timeSinceLastShift + shiftRate)         // Time.time a way to get the current time in Unity
        { */

        if (isCooled)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("teleporting");
                playerScript.MoveSpeedMultiplier *= 100;
                playerScript.regSpeed *= 200;
                isCooled = false;


                StartCoroutine(teleport());
                StartCoroutine(coolDown());

            }
        }
         /*   timeSinceLastShift = Time.time;
        } */

    }


    IEnumerator teleport()
    {
        yield return new WaitForSeconds(.015f);

        playerScript.MoveSpeedMultiplier /= 100;
        playerScript.regSpeed /= 200;
    }

    IEnumerator coolDown()
    {
        yield return new WaitForSeconds(2.0f);
        isCooled = true;
    }
}