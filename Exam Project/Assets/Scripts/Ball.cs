using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 initialVelocity;
    public ParticleSystem trail;
    public Score score;
    public GameObject bonusPrefab;

    // Sound effects
    public AudioClip bounceSfx;
    public AudioClip hitSfx;
    public AudioClip crashSfx;
    public AudioClip trailSfx;    

    private Rigidbody ballRb;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        ballRb.velocity = initialVelocity;
        audioSource = GetComponent<AudioSource>();
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
        trail.Play(); // This trail should be non-looping and last a few seconds
        Score(50);

        // Plays the trail SFX when the trail starts
        audioSource.PlayOneShot(trailSfx);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Plays the proper sound at the beginning of the collision
        if (collision.collider.CompareTag("Tile"))
        {
            audioSource.PlayOneShot(hitSfx);
        }
        else if (collision.collider.CompareTag("Floor"))
        {
            audioSource.PlayOneShot(crashSfx);
        }
        else
        {
            audioSource.PlayOneShot(bounceSfx);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Scores points at the end of the collision
        int bonus = 0;
        if (collision.collider.CompareTag("Tile"))
        {
            bonus = 200;
        }
        else if (collision.collider.CompareTag("Floor"))
        {
            bonus = -1000;
        }
        if (bonus != 0)
        {
            Score(bonus);
        }
    }

    private void Score(int pointsScored)
    {
        // Increases the score (or decreases it if pointsScored < 0)
        score.points += pointsScored;

        // Creates a floating label with the score change at the ball's position
        GameObject bonusLabel = Instantiate(bonusPrefab, transform.position, bonusPrefab.transform.rotation);
        string bonusLabelText;
        if (pointsScored >= 0)
        {
            // If it's an increase we want a plus sign before it
            bonusLabelText = "+" + pointsScored;
        }
        else
        {
            // If it's a decrease the minus sign is already there
            bonusLabelText = pointsScored.ToString();
        }
        bonusLabel.GetComponent<TextMesh>().text = bonusLabelText;
        // The TextFadeOut Component will take care of the graphic effects
    }
}
