using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRay_PlayerScript : MonoBehaviour {

	[System.Serializable]
	public class Definitions
	{
		[SerializeField]
		private string _XRayTag = "XRay";
		[SerializeField]
		private float _touchDistance = 1.0f;

		public string XRayTag
		{ 
			get { return _XRayTag; } 
			set 
			{ 
				if(_XRayTag == "")
					_XRayTag = value; 
			}
		}
		public float touchDistance
		{
			get { return _touchDistance; }
			set
			{
				if(value >= 0.0f)
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
				if(!_rayOrigin)
					_rayOrigin = value;
			}
		}
		public Shader transparent
		{
			get { return _transparent; }
			set
			{
				if(!_transparent)
					_transparent = value;
			}
		}
	}

	private bool isTouched;
	private bool _xrayActive;
	private bool canXray;
	[SerializeField]
	private bool useCoolDown = true;

	private float _coolDownTime;

	/// <summary>
	/// Returns if _xrayActive is true or false
	/// </summary>
	public bool xrayActive
	{
		get{ return _xrayActive;	}
	}

	/// <summary>
	/// Returns Cool Down Time (time until power can be used again)
	/// </summary>
	public float coolDownTime
	{
		get{ return _coolDownTime; }
	}

	private Renderer currentObject;
	private Shader currentShader;

	[SerializeField]
	private Definitions definitions = new Definitions ();
	[SerializeField]
	private DragDrop dragAndDropVariables = new DragDrop ();
	[SerializeField]
	private MonoBehaviour[] componentsToDisable;

	// Use this for initialization
	void Start () 
	{
		if(definitions.XRayTag == "")
		{
			Debug.LogError("No XRay_Tag string defined in the Inspector. Defaulting to XRay.");
			definitions.XRayTag = "XRay";
		}
		if(definitions.touchDistance <= 0.0f)
		{
			Debug.LogError("No Touch Distance set in the Inspector. Defaulting to 1.0f");
			definitions.touchDistance = 1.0f;
		}

		if(!dragAndDropVariables.rayOrigin)
		{
			Debug.LogError("No Character Controller script attached.");
		}
		if(!dragAndDropVariables.transparent)
		{
			Debug.LogError("No Transparent shader attached.");
		}	

		currentObject = null;
		currentShader = null;
		isTouched = false;
		_xrayActive = false;
		canXray = true;
		_coolDownTime = 0.0f;
	}

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			EnableXray();
		}

		if(Input.GetKeyUp(KeyCode.E))
		{
			DisableXray();
		}
	}

	/// <summary>
	/// Use this to turn xray-able objects transparent when in range of that object.
	/// </summary>
	public void EnableXray(float touchDistance = -1.0f)
	{
		if(canXray)
		{
			if(touchDistance >= 0.0f)
				definitions.touchDistance = touchDistance;

			Ray ray = new Ray (dragAndDropVariables.rayOrigin.position, transform.forward);
			Debug.DrawRay (ray.origin, ray.direction, Color.black);

			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, definitions.touchDistance) && hit.collider.tag == definitions.XRayTag) 
			{
				if(useCoolDown)
					StartCoroutine(TimeCounter());

				foreach(MonoBehaviour script in componentsToDisable)
				{
					script.enabled = false;
				}

				currentObject = hit.collider.GetComponent<Renderer> ();
				if(!isTouched)
				{
					currentShader = currentObject.material.shader;
					isTouched = true;
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
		if (currentObject && canXray) 
		{
			if(useCoolDown)
				canXray = false;

			foreach(MonoBehaviour script in componentsToDisable)
			{
				script.enabled = true;
			}

			_xrayActive = false;
			currentObject.material.shader = currentShader;
			isTouched = false;
			currentObject = null;
			currentShader = null;

			if(useCoolDown)
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
		if(_coolDownTime <= 0.0f)
		{
			canXray = true;
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
		if(_coolDownTime < 10)
		{
			StartCoroutine(TimeCounter());
		}
	}
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRay_PlayerScript : MonoBehaviour {

	static private string XRay_Tag = "XRay";

	private Transform rayOrigin;

	private bool isTouched;
	private bool xrayActive;

	private Renderer currentObject;
	private Shader currentShader;
	private Shader transparent;

	// Use this for initialization
	void Start () 
	{
		rayOrigin = transform.Find("RayOrigin").gameObject.transform;
		currentObject = null;

		transparent = Shader.Find("Transparent");

		isTouched = false;
		xrayActive = false;
	}

	// Update is called once per frame
	void Update ()
	{
		Ray ray = new Ray (rayOrigin.position, transform.forward);
		Debug.DrawRay (ray.origin, ray.direction, Color.black);

		RaycastHit hit;
		if (Input.GetKey (KeyCode.E) && Physics.Raycast (ray, out hit, 0.5f)) 
		{
			if (hit.collider.tag == XRay_Tag) {
				currentObject = hit.collider.GetComponent<Renderer> ();
				if(!isTouched)
				{
					currentShader = currentObject.material.shader;
					isTouched = true;
				}
				currentObject.material.shader = transparent;
				xrayActive = true;
			}
		} 
		else
		{
			xrayActive = false;
		}

		if (!xrayActive && currentObject) 
		{
			currentObject.material.shader = currentShader;
			isTouched = false;
			currentObject = null;
			currentShader = null;
		}
	}
}*/