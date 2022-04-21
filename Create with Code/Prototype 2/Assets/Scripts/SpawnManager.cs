using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // animalPrefabs[0], animalPrefabs[1]…
    public GameObject[] animalPrefabs;
    public KeyCode spawnKey;
    public float spawnOrthogonalRange;
    public float spawnDistanceFromCenter;

    private float startDelay = 2f;
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("SpawnRandomAnimalFromTop", startDelay, spawnInterval);
        InvokeRepeating("SpawnRandomAnimalFromLeft", startDelay * 2, spawnInterval * 1.3f);
        InvokeRepeating("SpawnRandomAnimalFromRight", startDelay * 3, spawnInterval * 1.4f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnRandomAnimalFromTop();
        }
    }

    private void SpawnRandomAnimalFromTop()
    {
        Vector3 spawnPos = new Vector3(
                 Random.Range(-spawnOrthogonalRange, spawnOrthogonalRange),
                 0,
                 spawnDistanceFromCenter
                 );
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(
            animalPrefabs[animalIndex],
            spawnPos,
            Quaternion.LookRotation(Vector3.back, Vector3.up));
    }

    private void SpawnRandomAnimalFromLeft()
    {
        Vector3 spawnPos = new Vector3(
                 -spawnDistanceFromCenter,
                 0,
                 Random.Range(-spawnOrthogonalRange, spawnOrthogonalRange)
                 );
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(
            animalPrefabs[animalIndex],
            spawnPos,
            Quaternion.LookRotation(Vector3.right, Vector3.up));
    }

    private void SpawnRandomAnimalFromRight()
    {
        Vector3 spawnPos = new Vector3(
                 spawnDistanceFromCenter,
                 0,
                 Random.Range(-spawnOrthogonalRange, spawnOrthogonalRange)
                 );
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(
            animalPrefabs[animalIndex],
            spawnPos,
            Quaternion.LookRotation(Vector3.left, Vector3.up));
    }
}
