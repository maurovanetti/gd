using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 initialVelocity;
    public ParticleSystem trail;

    private Rigidbody ballRb;

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        ballRb.velocity = initialVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        // Moves the trail emitter with the ball        
        trail.transform.position = transform.position;
        // The emitter must be simulated in World space
    }

    public void AttachTrail()
    {        
        // This trail should be non-looping and last a few seconds
        trail.Play();
    }
}
