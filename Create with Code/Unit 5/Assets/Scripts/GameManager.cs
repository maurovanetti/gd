using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float spawnRate = 1.0f;
    private int score = 0;
    private bool isGameOver = false;
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());
        UpdateScore(0);        
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameOver = true;
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    private IEnumerator SpawnTarget()
    {
        while (true)
        {
            if (!isGameOver)
            {
                yield return new WaitForSeconds(spawnRate);
                int targetIndex = Random.Range(0, targets.Count);
                GameObject randomPrefab = targets[targetIndex];
                Instantiate(randomPrefab);
                UpdateScore(5);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
