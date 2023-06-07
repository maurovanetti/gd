using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    private float spawnRate = 2.0f; // corresponds to Easy
    private int score = 0;

    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public GameObject titleScreenStuff;
    public GameObject gameOverStuff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        titleScreenStuff.SetActive(false);
        scoreText.gameObject.SetActive(true);
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
    }

    public void GameOver()
    {
        gameOverStuff.SetActive(true);
        isGameOver = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    private IEnumerator SpawnTarget()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int targetIndex = Random.Range(0, targets.Count);
            GameObject randomPrefab = targets[targetIndex];
            Instantiate(randomPrefab);
            UpdateScore(5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
