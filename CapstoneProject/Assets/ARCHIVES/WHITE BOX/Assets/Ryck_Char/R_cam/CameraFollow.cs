using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    // who to look at/follow
    public GameObject Target;
    // find player if none is specified
    public bool autoFindPlayer;
    // mouse x
    private float mx = 0.0f;
    // mouse y
    private float my = 0.0f;
    // horizontal mouse speed or horizontal look sensitivity
    private int mxSpeed = 5;
    // vertical mouse speed or vertical look sensitivity;
    private int mySpeed = 5;

    public float MaxViewDistance = 15f;
    public float MinViewDistance = 1f;
    public int ZoomRate = 20;
    private int lerpRate = 5;
    private float distance = 3f;
    private float desireDistance;
    private float correctedDistance;
    private float currentDistance;

    public float cameraTargetHeight = 1.0f;

    //checks if first person mode is on
    private bool click = false;
    //stores cameras distance from player
    private float distanceFromTarget = 0;
    public Rigidbody rbody;

    void Awake()
    {
        if (autoFindPlayer)
        {
            if (!Target)
            {
                Target = GameObject.FindGameObjectWithTag("Player");
            }
        }
        if (rbody)
        {
            rbody.freezeRotation = true;
        }
    }

    void Start()
    {
        Vector3 Angles = transform.eulerAngles;
        mx = Angles.x;
        my = Angles.y;
        currentDistance = distance;
        desireDistance = distance;
        correctedDistance = distance;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            mx += Input.GetAxis("Mouse X") * mxSpeed;
            my += Input.GetAxis("Mouse Y") * mySpeed;
        }
        else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            float targetRotantionAngle = Target.transform.eulerAngles.y;
            float cameraRotationAngle = transform.eulerAngles.y;
            mx = Mathf.LerpAngle(cameraRotationAngle, targetRotantionAngle, lerpRate * Time.deltaTime);
        }

        my = ClampAngle(my, -15, 25);
        Quaternion rotation = Quaternion.Euler(my, mx, 0);

        desireDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ZoomRate * Mathf.Abs(desireDistance);
        desireDistance = Mathf.Clamp(desireDistance, MinViewDistance, MaxViewDistance);
        correctedDistance = desireDistance;

        Vector3 position = Target.transform.position - (rotation * Vector3.forward * desireDistance);

        RaycastHit collisionHit;
        Vector3 cameraTargetPosition = new Vector3(Target.transform.position.x, Target.transform.position.y + cameraTargetHeight, Target.transform.position.z);

        bool isCorrected = false;
        if (Physics.Linecast(cameraTargetPosition, position, out collisionHit))
        {
            position = collisionHit.point;
            correctedDistance = Vector3.Distance(cameraTargetPosition, position);
            isCorrected = true;
        }

        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * ZoomRate) : correctedDistance;

        position = Target.transform.position - (rotation * Vector3.forward * currentDistance + new Vector3(0, -cameraTargetHeight, 0));

        transform.rotation = rotation;
        transform.position = position;

        float cameraX = transform.rotation.x;
        // Right click drag to control the camera's rotation.
        if (Input.GetMouseButton(1))
        {
            Target.transform.eulerAngles = new Vector3(cameraX, transform.eulerAngles.y, transform.eulerAngles.z);
        }

    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}