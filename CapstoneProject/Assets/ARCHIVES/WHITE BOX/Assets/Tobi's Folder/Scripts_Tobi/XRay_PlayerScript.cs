using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRay_PlayerScript : MonoBehaviour
{

    [System.Serializable]
    public class Definitions
    {
        [SerializeField]
        private float _touchDistance = 1.0f;
        
        public float touchDistance
        {
            get { return _touchDistance; }
            set
            {
                if (value >= 0.0f)
                    _touchDistance = value;
            }
        }
    }

    [System.Serializable]
    public class DragDrop
    {
        [SerializeField]
        private Transform _rayOrigin;
        [SerializeField]
        private Shader _transparent;

        public Transform rayOrigin
        {
            get { return _rayOrigin; }
            set
            {
                if (!_rayOrigin)
                    _rayOrigin = value;
            }
        }
        public Shader transparent
        {
            get { return _transparent; }
            set
            {
                if (!_transparent)
                    _transparent = value;
            }
        }
    }

    private bool _isTouched;
    private bool _xrayActive;
    private bool _canXray;
    [SerializeField]
    private bool _useCoolDown = false;

    private float _coolDownTime;

    /// <summary>
    /// Returns if _xrayActive is true or false
    /// </summary>
    public bool xrayActive
    {
        get { return _xrayActive; }
    }

    /// <summary>
    /// Returns Cool Down Time (time until power can be used again)
    /// </summary>
    public float coolDownTime
    {
        get { return _coolDownTime; }
    }

    private Renderer currentObject;
    private Shader currentShader;

    [SerializeField]
    private Definitions definitions = new Definitions();
    [SerializeField]
    private DragDrop dragAndDropVariables = new DragDrop();
    [SerializeField]
    private MonoBehaviour[] componentsToDisable;

    // Use this for initialization
    void Start()
    {
        if (definitions.touchDistance <= 0.0f)
        {
            Debug.LogError("No Touch Distance set in the Inspector. Defaulting to 1.0f");
            definitions.touchDistance = 1.0f;
        }

        if (!dragAndDropVariables.rayOrigin)
        {
            Debug.LogError("No ray origin attached.");
        }
        if (!dragAndDropVariables.transparent)
        {
            Debug.LogError("No Transparent shader attached.");
        }





        currentObject = null;
        currentShader = null;
        _isTouched = false;
        _xrayActive = false;
        _canXray = true;
        _coolDownTime = 0.0f;
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
    public void EnableXray(float touchDistance = -1.0f)
    {
        if (_canXray)
        {
            if (touchDistance >= 0.0f)
                definitions.touchDistance = touchDistance;

            Ray ray = new Ray(dragAndDropVariables.rayOrigin.position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction, Color.black);

            RaycastHit hit;

			if (Physics.Raycast(ray, out hit, definitions.touchDistance) && hit.collider.gameObject.GetComponent<XRay_ObjectScript>())
            {
                if (_useCoolDown)
                    StartCoroutine(TimeCounter());

                foreach (MonoBehaviour script in componentsToDisable)
                {
                    script.enabled = false;
                }

                currentObject = hit.collider.GetComponent<Renderer>();
                if (!_isTouched)
                {
                    currentShader = currentObject.material.shader;
                    _isTouched = true;
                }
                currentObject.material.shader = dragAndDropVariables.transparent;
                _xrayActive = true;
            }
        }
    }

    /// <summary>
    /// Use this to turn xray-able objects, that are currently transparent, solid again.
    /// </summary>
    public void DisableXray()
    {
        if (currentObject && _canXray)
        {
            if (_useCoolDown)
                _canXray = false;

            foreach (MonoBehaviour script in componentsToDisable)
            {
                script.enabled = true;
            }

            _xrayActive = false;
            currentObject.material.shader = currentShader;
            _isTouched = false;
            currentObject = null;
            currentShader = null;

            if (_useCoolDown)
            {
                StopAllCoroutines();
                StartCoroutine(CoolDown());
                //_coolDownTime = 0.0f;
            }
        }
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(1.0f);
        if (_coolDownTime <= 0.0f)
        {
            _canXray = true;
        }
        else
        {
            _coolDownTime -= 1.0f;
            StartCoroutine(CoolDown());
        }
    }

    private IEnumerator TimeCounter()
    {
        _coolDownTime += 1.0f;
        yield return new WaitForSeconds(1.0f);
        if (_coolDownTime < 10)
        {
            StartCoroutine(TimeCounter());
        }
    }
}