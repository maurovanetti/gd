using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PongBall : MonoBehaviour
{
    public Text scoreText;
    public Vector2 initialVelocity;
    private Rigidbody ballRb;
    private int playerScore;
    private int enemyScore;
    private bool playerScoredLast;

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        ballRb.velocity = initialVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player Goal"))
        {
            playerScore++;
            playerScoredLast = true;
            UpdateScore();
        }
        else if (other.CompareTag("Enemy Goal"))
        {
            enemyScore++;
            playerScoredLast = false;
            UpdateScore();
        }        
    }

    private void UpdateScore()
    {
        scoreText.text = $"{playerScore} - {enemyScore}";
        Invoke("Respawn", 2);
    }

    private void Respawn()
    {
        transform.position = new Vector3(0, 20, 0);
        if (playerScoredLast)
        {
            ballRb.velocity = initialVelocity;
        }
        else
        {
            ballRb.velocity = -initialVelocity;
        }
    }
}
