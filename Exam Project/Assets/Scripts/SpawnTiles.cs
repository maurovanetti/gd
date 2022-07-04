using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTiles : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float tileSideLength = 1.0f;
    public int rows = 10;
    public int columns = 10;


    // Start is called before the first frame update
    void Start()
    {
        Spawn(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn(Vector3 origin)
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                Vector3 position = new Vector3(
                    origin.x + (column * tileSideLength),
                    origin.y + (row * tileSideLength),
                    origin.z + Random.Range(-1f, 1f));
                GameObject prefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];
                GameObject tile = Instantiate(prefab, position, prefab.transform.rotation, 
                    transform); // Sets the spawner as the parent of the newly instantiated tile
                tile.name = "Tile " + row + "," + column;
            }
        }
    }
}
