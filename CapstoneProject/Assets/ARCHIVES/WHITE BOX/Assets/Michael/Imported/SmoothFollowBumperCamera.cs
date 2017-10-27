//(Created CSharp Version) 10/2010: Daniel P. Rossi (DR9885) 

using UnityEngine;
using System.Collections;

public class SmoothFollowBumperCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private float distance = 3.5f;
    [SerializeField]
    private float height = 1.0f;
    [SerializeField]
    private float damping = 5.0f;
    [SerializeField]
    private bool smoothRotation = true;
    [SerializeField]
    private float rotationDamping = 10.0f;
    [SerializeField]
    private Vector3 targetLookAtOffset; // allows offsetting of camera lookAt, very useful for low bumper heights
    [SerializeField]
    private float bumperDistanceCheck = 4.5f; // length of bumper ray
    [SerializeField]
    private float bumperCameraHeight = 1.0f; // adjust camera height while bumping
    [SerializeField]
    private Vector3 bumperRayOffset; // allows offset of the bumper ray from target origin

    /// <Summary>
    /// If the target moves, the camera should child the target to allow for smoother movement. DR
    /// </Summary>
    private void Awake()
    {
        this.transform.parent = target;
    }

    private void FixedUpdate()
    {
        float x, z, horDist;
        x = transform.position.x - target.position.x;
        z = transform.position.z - target.position.z;
        horDist = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2));
        //Debug.Log("Horizontal Dist: " + horDist);

        Vector3 wantedPosition = target.TransformPoint(0, height, -distance);

        // check to see if there is anything behind the target
        RaycastHit hit;
        Vector3 back = target.transform.TransformDirection(new Vector3(0f, 0f, -1f));

        // cast the bumper ray out from rear and check to see if there is anything behind
        if (Physics.Raycast(target.TransformPoint(bumperRayOffset), back, out hit, bumperDistanceCheck)
            && hit.transform != target) // ignore ray-casts that hit the user. DR
        {
            Debug.DrawLine(target.transform.position, hit.point, Color.red);

            // Increase height of camera if camera within minimum horizontal distance from player
            if (horDist < bumperDistanceCheck)
            {
                bumperCameraHeight = 1f;
            }
            else
            {
                bumperCameraHeight = 1f;
            }

            float xBuffer = 1f;
            float zBuffer = 1f;

            if (target.position.x - hit.point.x < 0)
                xBuffer = -1f;

            if (target.position.z - hit.point.z > 0)
                zBuffer = -1f;

            // clamp wanted position to hit position
            wantedPosition.x = hit.point.x + xBuffer;
            wantedPosition.z = hit.point.z + zBuffer;
            wantedPosition.y = Mathf.Lerp(hit.point.y + bumperCameraHeight, wantedPosition.y, Time.deltaTime * damping);
        }

        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

        Vector3 lookPosition = target.TransformPoint(targetLookAtOffset);

        if (smoothRotation)
        {
            Quaternion wantedRotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        }
        else
            transform.rotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
    }
}