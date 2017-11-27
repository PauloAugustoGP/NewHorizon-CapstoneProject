using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Passcode_ObjectScript : MonoBehaviour
{
    [SerializeField] private Text _codeText;
    [SerializeField] private Text _worldCodeText;

    public int[] passCode = new int[4];

    private int _playerCodeIndex = 0;
    private int[] _playerCode = new int[4];

    private bool _canInput = true;

    [SerializeField] private UnityEvent onPasscodeAccept;

    // Use this for initialization
    void Start()
    {
        GenerateCode();
        UpdateText();
        _worldCodeText.text = string.Format("{0}\t{1}\t{2}\t{3}", passCode[0], passCode[1], passCode[2], passCode[3]);
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
        if (!_canInput || _playerCodeIndex >= 4)
            return;

        _playerCode[_playerCodeIndex] = pEntry;
        ++_playerCodeIndex;

        UpdateText();
    }

    public void OnPasscodeCompaire()
    {
        int equal = 0;

        for (int i = 0; i < passCode.Length; ++i)
        {
            if (passCode[i] == _playerCode[i])
            {
                equal++;
            }
        }

        if (equal >= 4)
            Result(true);
        else
            Result(false);
    }

    void Result(bool pass)
    {
        if (pass)
        {
            Debug.Log("Codes are the same");
            _codeText.color = Color.green;
            onPasscodeAccept.Invoke();
        }
        else
        {
            Debug.Log("Codes do not match");
            _codeText.color = Color.red;
            StartCoroutine(Reset_playerCode());
        }
    }

    public void OnBackButton()
    {
        if (_playerCodeIndex > 0)
        {
            _playerCodeIndex--;
            _playerCode[_playerCodeIndex] = 0;
            UpdateText();
        }
    }

    void UpdateText()
    {
        _codeText.text = string.Format("{0}\t{1}\t{2}\t{3}", _playerCode[0], _playerCode[1], _playerCode[2], _playerCode[3]);
    }

    IEnumerator Reset_playerCode()
    {
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < _playerCode.Length; ++i)
        {
            _playerCode[i] = 0;
        }

        _playerCodeIndex = 0;
        _codeText.color = Color.black;
        UpdateText();
        _canInput = true;
    }
}
