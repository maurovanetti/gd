using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required in order to reload scene
using UnityEngine.UI; // Required in order to use Text

public class Score : MonoBehaviour
{
    public int points;
    
    private Text label;

    // Start is called before the first frame update
    void Start()
    {
        label = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (points < 0)
        {
            // Game over when the score goes below zero
            label.text = "GAME OVER - press R to retry";

            // Freezes time
            Time.timeScale = 0;
        }
        else
        {
            // Keep the score label in sync with the actual score
            label.text = points.ToString();
        }

        // Restarts by pressing the R key
        if (Input.GetKeyDown(KeyCode.R))
        {            
            // Unfreezes time
            Time.timeScale = 1;

            // Reloads the first and only scene of this project
            SceneManager.LoadScene(0);
        }
    }
}
