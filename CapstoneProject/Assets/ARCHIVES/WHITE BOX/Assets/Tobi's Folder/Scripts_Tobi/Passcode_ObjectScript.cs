﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passcode_ObjectScript : MonoBehaviour
{
    public Text codeText;

    public int[] playerCode = new int[4];
    public int[] passCode = new int[4];

    public int playerCodeIndex = 0;

    // Use this for initialization
    void Start()
    {
        GenerateCode();
        UpdateText();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            GenerateCode();
    }

    // Update is called once per frame
    void GenerateCode()
    {
        int[] usedNum = new int[20];

        for (int i = 0; i < passCode.Length; ++i)
        {
            passCode[i] = 10;
            usedNum[i] = 10;
        }

        int usedNumIndex = 0;
        for (int i = 0; i < passCode.Length; ++i)
        {
            int newNum = (int)Random.Range(0, 10);

            int used = 0;

            for (int j = 0; j < usedNum.Length; ++j)
            {
                if (newNum == usedNum[j])
                    used++;
            }

            if (used < 2)
            {
                usedNum[usedNumIndex] = newNum;
                usedNumIndex++;
                passCode[i] = newNum;
            }
            else
                --i;
        }
    }

    public void OnNumberAdd(int pEntry)
    {
        playerCode[playerCodeIndex] = pEntry;
        ++playerCodeIndex;

        UpdateText();

        if (playerCodeIndex >= 4)
        {
            playerCodeIndex = 0;
            PasscodeCompaire();
        }
    }

    void PasscodeCompaire()
    {
        int equal = 0;

        for (int i = 0; i < passCode.Length; ++i)
        {
            if (passCode[i] == playerCode[i])
            {
                equal++;
            }
        }

        if (playerCode[0] == 7 && playerCode[1] == 6 && playerCode[2] == 7 && playerCode[3] == 6)
            Result(true, "Popo");
        else if (equal >= 4)
            Result(true);
        else
            Result(false);
    }

    void Result(bool pass, string popo = "")
    {
        if (popo == "Popo")
        {
            Debug.Log("POPO!!!");
            codeText.color = Color.black;
        }
        else if (pass)
        {
            Debug.Log("Codes are the same");
            codeText.color = Color.green;
            GenerateCode();
        }
        else
        {
            Debug.Log("Codes do not match");
            codeText.color = Color.red;
        }

        StartCoroutine(ResetPlayerCode());
    }

    public void OnBackButton()
    {
        if (playerCodeIndex > 0)
        {
            playerCodeIndex--;
            playerCode[playerCodeIndex] = 0;
            UpdateText();
        }
    }

    void UpdateText()
    {
        codeText.text = string.Format("{0}\t{1}\t{2}\t{3}", playerCode[0], playerCode[1], playerCode[2], playerCode[3]);
    }

    IEnumerator ResetPlayerCode()
    {
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < playerCode.Length; ++i)
        {
            playerCode[i] = 0;
            playerCodeIndex = 0;
        }

        codeText.color = Color.black;
        UpdateText();
    }
}