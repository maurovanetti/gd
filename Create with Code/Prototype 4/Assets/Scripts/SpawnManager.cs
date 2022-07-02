using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject tougherEnemyPrefab;
    public GameObject defencePowerupPrefab;
    public GameObject attackPowerupPrefab;
    public GameObject evasionPowerupPrefab;
    public TextMeshProUGUI scoreLabel;
    public int toughEnemyPercentage; // 0 to 100
   
    private float spawnRange = 9;
    private float floorElevation = 0;
    private int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave();
    }    

    // Update is called once per frame
    void Update()
    {        
        if (FindObjectsOfType<Enemy>().Length == 0)
        {
            SpawnEnemyWave();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Spawn(enemyPrefab, "Extra Enemy");
        }
    }

    private void SpawnEnemyWave()
    {
        // Updates the score at each wave
        scoreLabel.text = waveNumber.ToString();
        Debug.Log("Wave started");
        for (int i = 0; i < waveNumber; i++)
        {
            if (Random.Range(0, 100) < toughEnemyPercentage)
            {
                Spawn(tougherEnemyPrefab, "Enemy #" + i + " [tough]");                
            }
            else
            {
                Spawn(enemyPrefab, "Enemy #" + i);
            }
        }
        Spawn(attackPowerupPrefab, "Attack Powerup #" + waveNumber);
        Spawn(defencePowerupPrefab, "Defence Powerup #" + waveNumber);
        Spawn(evasionPowerupPrefab, "Evasion Powerup #" + waveNumber);
        Debug.Log("Wave completed");
        waveNumber++;
    }

    private void Spawn(GameObject prefab, string objectName)
    {
        GameObject newEnemy = Instantiate(prefab,
            GenerateSpawnPosition(),
            prefab.transform.rotation);
        newEnemy.name = objectName;
        Debug.Log(newEnemy.name + " spawned");
    }    

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, floorElevation, spawnPosZ);
    }
}
