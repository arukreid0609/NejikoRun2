using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    Rigidbody rd;
    // Start is called before the first frame update
    void Start()
    {
        // rd = GetComponent<Rigidbody>();
        // rd.velocity = transform.forward * 10;
        Vector3 size = GetComponent<MeshRenderer>().bounds.size;
        Debug.Log(size);
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 10);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Vector3 r = Vector3.Reflect(rd.velocity, other.transform.forward * -1);
        // float angle = Vector3.Angle(rd.velocity, r);
        // transform.LookAt(r);
        // rd.velocity = r;
        // Debug.Log(angle);

    }
    private void OnCollisionEnter(Collision other)
    {
        Vector3 r = Vector3.Reflect(rd.velocity, other.transform.forward * -1);
        transform.LookAt(r);
        rd.velocity = r;
        Debug.Log(r);
    }
}
