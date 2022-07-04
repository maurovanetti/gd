using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float powerShotForce = 20f;
    public GameObject leftWall;
    public GameObject rightWall;   
    public float minDistanceFromWall;
    public ParticleSystem playerParticles;

    private Animator playerAnim;
    private Rigidbody playerRb;    
    private bool headingRight;
    private bool powerShotReady;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        headingRight = true;
        powerShotReady = false;
    }    

    // FixedUpdate is called at every fixed step of the physics calculation
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)) // Powering up for strong shooting
        {
            // Emits particles to show that the avatar is powering up
            playerParticles.transform.position = transform.position + Vector3.up * 3f;
            playerParticles.Play();
            powerShotReady = true;

            // Stands still when preparing for power shot
            playerRb.velocity = Vector3.zero;          
        }
        else // Not powering up, just ordinary running around and bouncing ball
        {
            // Stop emitting particles
            playerParticles.Stop();
            powerShotReady = false;

            // Reads the horizontal input (right/left arrows or right/left joystick movement) 
            float horizontalInput = Input.GetAxis("Horizontal");
            
            // Checks the boundaries
            float minX = leftWall.transform.position.x + minDistanceFromWall;
            float maxX = rightWall.transform.position.x - minDistanceFromWall;
            if (transform.position.x <= minX) // Left of the boundary
            {
                transform.position.Set(minX, transform.position.y, transform.position.z);
                if (horizontalInput < 0)
                {
                    horizontalInput = 0;
                }
            }
            else if (transform.position.x >= maxX) // Right of the boundary 
            {
                transform.position.Set(maxX, transform.position.y, transform.position.z);
                if (horizontalInput > 0)
                {
                    horizontalInput = 0;
                }
            }

            // Corrects the right/left heading, if it changed
            if (headingRight == true && horizontalInput < 0) // Heading right but going leftward
            {
                headingRight = false;
                transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
            }
            else if (headingRight == false && horizontalInput > 0) // Heading left but going rightward
            {
                headingRight = true;
                transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
            }

            // Moves the player's avatar at the proper horizontal speed
            playerRb.velocity = Vector3.right * horizontalInput * maxSpeed;
        }
    }

    // Update is called once per frame
    void Update() {
        // Sets the animation variable used to select Idle or Walk or Run
        playerAnim.SetFloat("Speed_f", playerRb.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (powerShotReady)
        {
            if (collision.collider.CompareTag("Ball"))
            {
                GameObject ball = collision.gameObject;
                Rigidbody ballRb = ball.GetComponent<Rigidbody>();

                // Adds force to the ball to make it bounce back
                Vector3 playerToBall = ball.transform.position - transform.position;                
                ballRb.AddForce(playerToBall.normalized * powerShotForce, ForceMode.Impulse);

                ball.GetComponent<Ball>().AttachTrail();
            }
        }
    }
}
