using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Passcode_ObjectScript : MonoBehaviour
{
    [SerializeField] private Text _codeText;
    [SerializeField] private Text[] _worldCodeText;

    private Passcode[] _passcodes;

    //public int[] passCode = new int[4];

    private int _correctPasscodeIndex = 0;
    private int _playerCodeIndex = 0;
    private int[] _playerCode = new int[4];

    private bool _canInput = true;

    [SerializeField] private UnityEvent[] onPasscodeAccept;

    // Use this for initialization
    void Start()
    {
        _passcodes = new Passcode[_worldCodeText.Length];

        for (int i = 0; i < _passcodes.Length; ++i)
        {
            _passcodes[i] = new Passcode();
            _worldCodeText[i].text = string.Format("{0}\t{1}\t{2}\t{3}", _passcodes[i].passCode[0], _passcodes[i].passCode[1], _passcodes[i].passCode[2], _passcodes[i].passCode[3]);
        }

        UpdateText();
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
        for (int i = 0; i < _passcodes.Length; ++i)
        {
            int equal = 0;

            for (int j = 0; j < _passcodes[i].passCode.Length; ++j)
            {
                if (_passcodes[i].passCode[j] == _playerCode[j])
                {
                    Debug.LogFormat("Your code: {0}, Their code: {1}", _passcodes[i].passCode[j], _playerCode[j]);
                    equal++;
                }
            }

            Debug.Log(equal);

            if (equal >= 4)
            {
                _correctPasscodeIndex = i;
                Result(true);
                return;
            }
        }

        Result(false);
    }

    void Result(bool pass)
    {
        if (pass)
        {
            Debug.Log("Codes are the same");
            _codeText.color = Color.green;
            onPasscodeAccept[_correctPasscodeIndex].Invoke();
            StartCoroutine(Reset_playerCode());
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

    public class Passcode
    {
        public int[] passCode = new int[4];

        public Passcode()
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
    }
}
