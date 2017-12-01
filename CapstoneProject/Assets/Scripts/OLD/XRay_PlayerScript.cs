using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRay_PlayerScript : MonoBehaviour
{
    /*
	Instructions:
	1: Add this script onto the player.
	2: Touch distance is the length the ray will shoot out of the player to see if you are close enough to an xrayable object
	3: Add an empty gameobject to the player. Place it where you want to ray check to start from then drah that gameobject into 
		the drag and drop variables for RayOrigin
	4: Click the cirlce to the right of the Shader field and select the shader named "Transparent"
	5: Place the script called "XRay_ObjectScript onto and object you want to be xrayed.
	6: Drag every componenet you want disabled into the componenets to disable array. These componenets will be deactivated while this skill is being used.
	7: Finished :)
	*/

    [Header("Definitions")]
    [SerializeField]
    private float _touchDistance = 1.0f;

    [Header("Drag and Drop")]
    [SerializeField]
    private Transform _rayOrigin;
    [SerializeField]
    private Shader _transparent;

    [Header("Disable")]
    [SerializeField]
    private MonoBehaviour[] componentsToDisable;

    private bool _isTouched;
    private bool _xrayActive;
    private bool _canXray;

    private Renderer _currentObject;
    private Shader _currentShader;

    /// <summary>
    /// Returns if _xrayActive is true or false
    /// </summary>
    public bool xrayActive
    {
        get { return _xrayActive; }
    }

    // Use this for initialization
    void Start()
    {
        if (_touchDistance <= 0.0f)
        {
            Debug.LogError("No Touch Distance set in the Inspector. Defaulting to 1.0f");
            _touchDistance = 1.0f;
        }

        if (!_rayOrigin)
        {
            Debug.LogError("No ray origin attached.");
        }
        if (!_transparent)
        {
            Debug.LogError("No Transparent shader attached.");
        }

        _currentObject = null;
        _currentShader = null;
        _isTouched = false;
        _xrayActive = false;
        _canXray = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EnableXray();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            DisableXray();
        }
    }

    /// <summary>
    /// Use this to turn xray-able objects transparent when in range of that object.
    /// </summary>
    public void EnableXray()
    {
        if (_canXray)
        {
            Ray ray = new Ray(_rayOrigin.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _touchDistance) && hit.collider.gameObject.GetComponent<XRay_ObjectScript>())
            {
                SetComponents(false);

                _currentObject = hit.collider.GetComponent<Renderer>();

                if (!_isTouched)
                {
                    _currentShader = _currentObject.material.shader;
                    _isTouched = true;
                }
                _currentObject.material.shader = _transparent;
                _xrayActive = true;
            }
        }
    }

    /// <summary>
    /// Use this to turn xray-able objects, that are currently transparent, solid again.
    /// </summary>
    public void DisableXray()
    {
        if (_currentObject && _canXray)
        {
            SetComponents(true);

            _xrayActive = false;
            _currentObject.material.shader = _currentShader;
            _isTouched = false;
            _currentObject = null;
            _currentShader = null;
        }
    }

    private void SetComponents(bool pState)
    {
        if (componentsToDisable.Length <= 0)
            return;

        foreach (MonoBehaviour script in componentsToDisable)
        {
            script.enabled = pState;
        }
    }
}