using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPushBall : MonoBehaviour
{
    public float strength;
    public bool alwaysRightward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 pushDirection = transform.right;
            if (alwaysRightward)
            {
                if (pushDirection.x < 0)
                {
                    pushDirection = -transform.right;
                }
            } 
            else // alwaysLeftward
            {
                if (pushDirection.x > 0)
                {
                    pushDirection = -transform.right;
                }
            }
            ballRb.AddForce(strength * pushDirection, ForceMode.Impulse);
        }
    }
}
