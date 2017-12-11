using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadObject : MonoBehaviour
{
    [SerializeField] private Canvas _keypadCanvas;

    private bool _inRange = false;

    private GameObject _player;

    void Start()
    {
        if (!_keypadCanvas)
            Debug.LogErrorFormat("KeypadObject Error: No keypad canvas in inspector for {0}.", gameObject.name);
        else
            _keypadCanvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _inRange)
        {
            if (_player.GetComponent<XRayPlayer>().xrayActive)
                return;

            if (_keypadCanvas.enabled)
            {
                Debug.Log("Leaving keypad.");
                DisablePlayer(false);
                Cursor.lockState = CursorLockMode.Locked;
                _keypadCanvas.enabled = false;
            }
            else if (!_keypadCanvas.enabled)
            {
                Debug.Log("Starting keypad.");
                DisablePlayer(true);
                Cursor.lockState = CursorLockMode.Confined;
                _keypadCanvas.enabled = true;
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            if (!_player)
                _player = c.gameObject;

            _inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        _inRange = false;
    }

    void DisablePlayer(bool pSetDisable)
    {
        foreach (MonoBehaviour script in _player.GetComponents<MonoBehaviour>())
        {
            script.enabled = !pSetDisable;
        }
    }
}
