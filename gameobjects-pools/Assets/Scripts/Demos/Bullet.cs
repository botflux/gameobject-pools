using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    
    private void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    public void Initialize (Quaternion rotation, float speed)
    {
        this.speed = speed;
        this.transform.rotation = rotation;
        this.transform.position = Vector3.zero;
    }    
}
