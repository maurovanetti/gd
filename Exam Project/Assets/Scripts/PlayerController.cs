using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public GameObject leftWall;
    public GameObject rightWall;   
    public float minDistanceFromWall;

    private Animator playerAnim;
    private Rigidbody playerRb;
    private bool headingRight;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        headingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float minX = leftWall.transform.position.x + minDistanceFromWall;
        float maxX = rightWall.transform.position.x - minDistanceFromWall;        
        if (transform.position.x <= minX)
        {
            transform.position.Set(minX, transform.position.y, transform.position.z);
            if (horizontalInput < 0)
            {
                horizontalInput = 0;
            }
        }
        else if (transform.position.x >= maxX)
        {
            transform.position.Set(maxX, transform.position.y, transform.position.z);
            if (horizontalInput > 0)
            {
                horizontalInput = 0;
            }
        }
        transform.Translate(Vector3.right * horizontalInput * maxSpeed * Time.deltaTime, Space.World);

        if (headingRight == true && horizontalInput < 0)
        {
            headingRight = false;
            transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
        }
        else if (headingRight == false && horizontalInput > 0)
        {
            headingRight = true;
            transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
        }

        float animationSpeed;
        if (horizontalInput > 0)
        {
            animationSpeed = horizontalInput;
        }
        else
        {
            animationSpeed = -horizontalInput;
        }
        playerAnim.SetFloat("Speed_f", animationSpeed);
    }
}
