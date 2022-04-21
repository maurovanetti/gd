using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // animalPrefabs[0], animalPrefabs[1]…
    public GameObject[] animalPrefabs;
    public KeyCode spawnKey;
    public float spawnRangeX;
    public float spawnPosZ;

    private float startDelay = 2f;
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnRandomAnimal();
        }
    }

    private void SpawnRandomAnimal()
    {
        Vector3 spawnPos = new Vector3(
                 Random.Range(-spawnRangeX, spawnRangeX),
                 0,
                 spawnPosZ
                 );
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(
            animalPrefabs[animalIndex],
            spawnPos,
            animalPrefabs[animalIndex].transform.rotation);
    }


}
