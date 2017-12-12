using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayPlayer : MonoBehaviour
{
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

    private bool _xrayActive;

    [SerializeField] private Renderer _currentObject;
    [SerializeField] private Shader _currentShader;

    [SerializeField] private List<Renderer> _currentObjects;
    [SerializeField] private List<Shader> _currentShaders;

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
        _xrayActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            EnableXray();

        if (Input.GetKeyUp(KeyCode.E))
            DisableXray();
    }

    /// <summary>
    /// Use this to turn xray-able objects transparent when in range of that object.
    /// </summary>
    public void EnableXray()
    {
        Ray ray = new Ray(_rayOrigin.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _touchDistance) && hit.collider.gameObject.GetComponent<XRayObject>())
        {
            SetComponents(false);
            _currentObject = hit.collider.GetComponent<Renderer>();
            _currentShader = _currentObject.material.shader;
            _currentObject.material.shader = _transparent;
            _xrayActive = true;
        }
        else if (Physics.Raycast(ray, out hit, _touchDistance) && hit.collider.gameObject.GetComponentInParent<XRayObject>())
        {
            SetComponents(false);
            GameObject parent = hit.collider.transform.parent.gameObject;

            foreach (Renderer renderer in parent.GetComponentsInChildren<Renderer>())
            {
                _currentObjects.Add(renderer);
            }

            foreach (Renderer renderer in _currentObjects)
            {
                _currentShaders.Add(renderer.material.shader);
                renderer.material.shader = _transparent;
            }

            _xrayActive = true;
        }
    }

    /// <summary>
    /// Use this to turn xray-able objects, that are currently transparent, solid again.
    /// </summary>
    public void DisableXray()
    {
        if (_currentObject)
        {
            SetComponents(true);
            _xrayActive = false;
            _currentObject.material.shader = _currentShader;
            _currentObject = null;
            _currentShader = null;
        }
        else if (_currentObjects.Count > 0)
        {
            SetComponents(true);
            _xrayActive = false;

            for (int i = 0; i < _currentObjects.Count; ++i)
            {
                _currentObjects[i].material.shader = _currentShaders[i];
            }

            _currentObjects.Clear();
            _currentShaders.Clear();
        }
    }

    private void SetComponents(bool pState)
    {
        if (componentsToDisable.Length <= 0)
            return;

        foreach (MonoBehaviour script in componentsToDisable)
            script.enabled = pState;
    }
}