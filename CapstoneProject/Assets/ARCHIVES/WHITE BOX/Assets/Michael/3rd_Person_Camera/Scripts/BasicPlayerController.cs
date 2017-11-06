using UnityEngine;

public class BasicPlayerController : MonoBehaviour {

    // Determines basic player controls or complete player controls
    // Basic: Ignores collision, forward, backward, strafe left and right
    // Complete: Collision, WASD, run vs. crouch
    [SerializeField]
    private bool _simpleMovement;

    [SerializeField]
    private float _basicSpeed;

	void Start () {

        _simpleMovement = true;
        _basicSpeed = 10f;

	}
	
	void Update () {

        if (_simpleMovement)
        {
            float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * _basicSpeed;
            float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * _basicSpeed;

            transform.Translate(x, 0, 0);
            transform.Translate(0, 0, z);
        }
	}
}
