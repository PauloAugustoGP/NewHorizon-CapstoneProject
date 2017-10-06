using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPower : MonoBehaviour
{
    [SerializeField] Transform vortex;
    float vortexAngle;
    [SerializeField][Range(1, 5)] float vortexSpeed;

    [SerializeField] ParticleSystem effect;

    List<GameObject> objects;
    
	void Start ()
    {
        vortexAngle = 0.0f;
        vortexSpeed = 2.0f;

        effect.Stop();

        objects = new List<GameObject>();
	}
	
	void Update ()
    {
        vortexAngle += vortexSpeed * Time.deltaTime;
        if (vortexAngle >= 360) vortexAngle = 0;

        vortex.rotation = Quaternion.Euler(-90.0f, vortexAngle, 0.0f);

        for (int i = 0; i < objects.Count; i++)
        {
            Vector3 direction = transform.position - objects[i].transform.position;

            float gravityForce = Physics.gravity.magnitude * ((GetComponent<Rigidbody>().mass * objects[i].GetComponent<Rigidbody>().mass) / direction.magnitude);
            gravityForce /= objects[i].GetComponent<Rigidbody>().mass;

            objects[i].GetComponent<Rigidbody>().velocity += direction.normalized * gravityForce * Time.deltaTime;

            if(Vector3.Distance(transform.position, objects[i].transform.position) < 0.5f)
            {
                effect.Play();
                Destroy(objects[i]);
                objects.RemoveAt(i);
            }
        }
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag != "Solid")
            objects.Add(c.gameObject);

    }

    void OnTriggerExit(Collider c)
    {
        for(int i = 0; i < objects.Count; i++)
        {
            if(c.gameObject.name == objects[i].name)
            {
                objects.RemoveAt(i);
            }
        }

    }
}
