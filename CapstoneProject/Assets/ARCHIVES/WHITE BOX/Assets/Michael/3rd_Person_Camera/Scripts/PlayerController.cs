using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform player;
    public float speed;
    //public float rotSpeed;

	void Start () {
        speed = 10f;
        //rotSpeed = 150f;
	}
	
	void Update () {
        //float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotSpeed;
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        //transform.Rotate(0, x, 0);
        transform.Translate(x, 0, 0);
        transform.Translate(0, 0, z);
	}
}
