using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Camera cam;
    public bool autoFindMainCamera = true;
    public bool useMainCam = true;
    [SerializeField]
    List<Camera> cameras;
    [SerializeField]
    charControlScript character;
    [SerializeField]
    List<Camera> characterCamera;
    public bool autoFindPlayer = true;
	// Use this for initialization
	void Start () {
		if(autoFindMainCamera) {
            FindMainCamera();
        }
        if(autoFindPlayer) {
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<charControlScript>();
        }
        FindAllCameras();
        FindCharacterCamera(character);
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void XrayCam() {

    }

    public void FindMainCamera() {
        cam = Camera.main;
        if(!cam) {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            if(!cam) {
                cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                if(!cam) {
                    Debug.LogError("[CameraScript] » FindMainCamera(); Error finding camera.");
                }
            }
        }
    }

    public void FindAllCameras() {
        Camera[] allCams = Camera.allCameras;
        if(allCams.Length <= 0) {
            Debug.LogError("[CameraScript] » FindAllCameras(); Could not find any camera in the game.");
        } else {
            Debug.Log("[CameraScript] » FindAllCameras(); Successfully found " + allCams.Length + " camera in the game.");
            foreach(Camera ac in allCams) {
                if(cameras.Contains(ac)) {
                    Debug.LogWarning("[CameraScript] » FindAllCameras(); " + ac.name + " already exists in the cameras List. Skipped.");
                } else {
                    cameras.Add(ac);
                }
            }
            if(cameras.Count <= 0) {
                Debug.Log("[CameraScript] » FindAllCameras(); Failed to add " + allCams.Length + " cameras to the cameras List.");
            } else {
                Debug.Log("[CameraScript] » FindAllCameras(); Successfully added " + allCams.Length + " cameras to the cameras List.");
            }
        }
    }

    public void FindCharacterCamera(charControlScript c) {
        try {
            if(!c) {
                c = character;
            }
            Camera[] playerCamera = c.gameObject.GetComponentsInChildren<Camera>();
            if(playerCamera.Length <= 0) {
                Debug.LogError("[CameraScript] » FindCharacterCamera(); Could not find any camera in Character `" + c.name);
                return;
            } else {
                Debug.Log("[CameraScript] » FindCharacterCamera(); Found " + playerCamera.Length + " camera in " + c.name);
                foreach(Camera cams in playerCamera) {
                    if(characterCamera.Contains(cams)) {
                        Debug.LogWarning("[CameraScript] » FindCharacterCamera(); " + cams.name + " already exists in the characterCamera List. Skipped.");
                    } else {
                        characterCamera.Add(cams);
                    }
                }
                if(characterCamera.Count <= 0) {
                    Debug.LogError("[CameraScript] » FindCharacterCamera(); Failed to add " + playerCamera.Length + " cameras from " + c.name + " to the characterCamera List.");
                } else {
                    Debug.Log("[CameraScript] » FindCharacterCamera(); Successfully added " + playerCamera.Length + " camera from " + c.name + " to the characterCamera List.");
                }
            }
        } catch (CameraScriptException cse) {
            Debug.Log("[CameraScriptException] " + cse.Message);
        }
    }

    public void SwitchCamera(Camera newCam) {
        Camera activeCam;
        if(useMainCam) {
            activeCam = mainCamera;
        }
        activeCam = cam;
        if(activeCam.enabled) {
            activeCam.enabled = false;
        }
        cam = newCam;
        useMainCam = false;
        if(cam == activeCam) {
            Debug.Log("[CameraScript] » SwitchCamera(); Failed to switch camera.");
        } else {
            Debug.Log("[CameraScript] » SwitchCamera(); Succesfully switched camera from " + activeCam.name + " to " + cam.name + ".");
        }
    }

    public void moveCam(Camera thisCam) {
        if(useMainCam) {
            thisCam = mainCamera;
        }
        if(!thisCam) {
            thisCam = cam;
        }
        if(character) {
            Vector3 offset;
            offset = thisCam.transform.position - character.transform.position;
            thisCam.transform.position = character.transform.position + offset;
        } else {
            Debug.LogError("[CameraScript] » moveCam(); Character not found.");
        }
    }
}
