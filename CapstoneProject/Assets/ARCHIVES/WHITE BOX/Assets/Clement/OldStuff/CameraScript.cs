using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    Camera mainCamera;
    Camera currentCam;
    charControlScript character;
    List<Camera> allCameras;
    List<Camera> characterCamera;

    public Vector3 fpsCamNode;
    public Vector3 tpsCamNode;
    public Vector3 startPos;

    public Vector2 sensitivity = new Vector2(15f, 15f);

    public bool autoFindMainCamera = false;
    public bool useMainCamera = false;
    public bool togglePanning = false;
    public bool xrayMode = false;
    public bool getCams = false;

    public float maxZoomLimit = 120f;
    public float minZoomLimit = 60f;
    public float panSpeed = 4f;
    public float rotateSpeed = 4f;
    public float swoopSpeed = 5f;

    class PrevCam {
        Camera _occ;
        Vector3 _ocv;

        public Camera occ {
            get { return _occ; }
            set { _occ = value; }
        }
        public Vector3 ocv {
            get { return _ocv; }
            set { _ocv = value; }
        }
    }
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        tpsCamNode = GameObject.Find("tpsCamNode").transform.position;
        fpsCamNode = GameObject.Find("fpsCamNode").transform.position;
        getCams = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(togglePanning) {
            Panning();
        }
        if(getCams) {
            StartCoroutine(getCameras());
        }
        if(xrayMode) {
            XrayActive();
        }
	}

    public void Rotate() {
        float xRotation = Input.GetAxis("Horizontal") * sensitivity.x;
        float yRotation = Input.GetAxis("Horizontal") * sensitivity.x;

        currentCam.transform.localEulerAngles = new Vector3(xRotation, 0, yRotation);
    }

    public void XrayActive() {
        xrayMode = !xrayMode;
        if(xrayMode) {
            if (transform.position != fpsCamNode) {
                Debug.Log("Moving the camera to the fpsNode");
                //transform.Translate(fpsCamNode * Time.deltaTime);
                Debug.Log("Setting the field of view to 90 for zoom effect.");
                currentCam.fieldOfView = 90;
                Debug.Log("Turning panning on.");
                togglePanning = true;
            }
        } else {
            if(transform.position != tpsCamNode) {
                Debug.Log("Moving the camera back to the original position");
                //transform.Translate(startPos * Time.deltaTime);
                Debug.Log("Resetting the field of view.");
                currentCam.fieldOfView = 60;
                Debug.Log("Turning Panning off.");
                togglePanning = false;
            }
        }
    }

    public void Panning() {
        float mx = Input.GetAxis("Mouse X") * sensitivity.x;
        float my = Input.GetAxis("Mouse Y") * sensitivity.y;
        //transform.Rotate(-my, mx, 0);
        mx *= Time.deltaTime;
        my *= Time.deltaTime;
        transform.Translate(0, 0, my);
        transform.Rotate(0, mx, 0);
    }

    public void FindAllCameras() {
        Camera[] allCams = Camera.allCameras;
        if(allCams.Length <= 0) {
            Debug.LogError("[CameraScript] » FindAllCameras(); Could not find any camera in the game.");
        } else {
            Debug.Log("[CameraScript] » FindAllCameras(); Successfully found " + allCams.Length + " camera in the game.");
            foreach(Camera ac in allCams) {
                if(allCameras.Contains(ac)) {
                    Debug.LogWarning("[CameraScript] » FindAllCameras(); " + ac.name + " already exists in the cameras List. Skipped.");
                } else {
                    allCameras.Add(ac);
                }
            }
            if(allCameras.Count <= 0) {
                Debug.Log("[CameraScript] » FindAllCameras(); Failed to add " + allCams.Length + " cameras to the cameras List.");
            } else {
                Debug.Log("[CameraScript] » FindAllCameras(); Successfully added " + allCams.Length + " cameras to the cameras List.");
            }
        }
    }

    public void FindCharacterCamera() {
        try {
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<charControlScript>();
            Camera[] playerCamera = character.gameObject.GetComponentsInChildren<Camera>();
            if(playerCamera.Length <= 0) {
                Debug.LogError("[CameraScript] » FindCharacterCamera(); Could not find any camera in Character `" + character.name);
                return;
            } else {
                Debug.Log("[CameraScript] » FindCharacterCamera(); Found " + playerCamera.Length + " camera in " + character.name);
                foreach(Camera cams in playerCamera) {
                    if(characterCamera.Contains(cams)) {
                        Debug.LogWarning("[CameraScript] » FindCharacterCamera(); " + cams.name + " already exists in the characterCamera List. Skipped.");
                    } else {
                        characterCamera.Add(cams);
                    }
                }
                if(characterCamera.Count <= 0) {
                    Debug.LogError("[CameraScript] » FindCharacterCamera(); Failed to add " + playerCamera.Length + " cameras from " + character.name + " to the characterCamera List.");
                } else {
                    Debug.Log("[CameraScript] » FindCharacterCamera(); Successfully added " + playerCamera.Length + " camera from " + character.name + " to the characterCamera List.");
                }
            }
        } catch (CameraScriptException cse) {
            Debug.Log("[CameraScriptException] " + cse.Message);
        }
    }

    IEnumerator getCameras(float delay = 10f) {
        yield return new WaitForSeconds(delay);
        Debug.Log("Searching for new cameras.");
        FindAllCameras();
        FindCharacterCamera();
    }
}
