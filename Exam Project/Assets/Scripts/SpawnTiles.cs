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
        // Spawn the tiles starting from the SpawnTiles object's position
        Spawn(transform.position);
    }

    // Update is called once per frame
    void Update()
    {       
    }

    private void Spawn(Vector3 origin)
    {
        // For each row
        for (int row = 0; row < rows; row++)
        {
            // And for each column in each row
            for (int column = 0; column < columns; column++)
            {
                // Calculates the next tile's spawn position
                Vector3 position = new Vector3(
                    origin.x + (column * tileSideLength), // Horizontal position in row
                    origin.y + (row * tileSideLength), // Vertical position in column
                    origin.z + Random.Range(-1f, 1f)); // Random variation along the Z axis

                // Picks a random tile prefab
                GameObject prefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];

                // Creates a new tile in the calculated position
                GameObject tile = Instantiate(prefab, position, prefab.transform.rotation, 
                    transform); // Sets the spawner as the parent of the newly instantiated tile

                // Gives the newly created tile a fancy, unique name
                tile.name = "Tile " + row + "," + column;
            }
        }
    }
}
