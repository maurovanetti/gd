using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Parameters
    public float jumpForce;
    public float gravityModifier;
    public int jumps = 0; // 0 = on ground
    public int maxJumps = 2;
    public float maxFallSpeedToEnableJump = 1f;
    // Initialized in editor

    // States
    public bool intro = true;
    public bool gameOver = false;
    public bool isDashing = false;
    private float score = 0;
    // Read by other components

    // References to other objects' components
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;    
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public TMPro.TextMeshProUGUI scoreText;    
    // Initialized in editor

    // References to this object's components
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    // Initialized in Start()    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {        
        if (intro)
        {            
            if (transform.position.x < 0)
            {
                dirtParticle.Stop();
                playerAnim.SetFloat("Speed_f", 0.3f);
                transform.Translate(Vector3.right * 1.5f * Time.deltaTime, Space.World);
            }
            else
            {
                dirtParticle.Play();
                intro = false;
                playerRb.velocity = Vector3.zero;                
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumps < maxJumps && !gameOver)
            {
                // Jumps
                jumps++;
                if (playerRb.velocity.y >= -maxFallSpeedToEnableJump)
                {                    
                    playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    playerAnim.SetTrigger("Jump_trig");
                    dirtParticle.Stop();
                    playerAudio.PlayOneShot(jumpSound);
                }
            }

            if (!gameOver)
            {
                if (Input.GetKey(KeyCode.LeftShift) && jumps == 0)
                {
                    isDashing = true;
                    playerAnim.SetFloat("Speed_f", 2);
                    score += Time.deltaTime * 2;
                }
                else
                {
                    isDashing = false;
                    playerAnim.SetFloat("Speed_f", 1);
                    score += Time.deltaTime;
                }
                scoreText.text = Mathf.RoundToInt(score).ToString();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {            
            jumps = 0;
            dirtParticle.Play();
            // TODO Add landing SFX
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound);
        }
        
    }
}
