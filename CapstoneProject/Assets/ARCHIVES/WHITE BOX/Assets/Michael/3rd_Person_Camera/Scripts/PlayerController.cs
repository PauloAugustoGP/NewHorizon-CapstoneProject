using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

	void Start () {

        speed = 10f;

	}
	
	void Update () {

        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        transform.Translate(x, 0, 0);
        transform.Translate(0, 0, z);
	}
}
