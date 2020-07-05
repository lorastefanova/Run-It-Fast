using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] groundPrefabs; // Size of prefabs

    private Transform playerTransform; // Reference to the player
    private float spawnZ = 0.0f;       // Variable for where on z should we spawn objects
    private float tileLength = 21.0f;  // Variable for the length of the ground/tile
    private int tilesOnScreen = 4;     // Variable for how many tiles are on the screen 
    private float safeZone = 25.0f;    // Variable to delete a little after the player crosses the tile
    private int lastGroundIndex = 0;   // Variable for randomizing tiles

    private List<GameObject> activeTiles; // List of tiles

    // Start is called before the first frame update
    private void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Player position

        // Spawn tiles 
        for (int i = 0; i < tilesOnScreen; i++)
        {
            if (i < 1)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile();
            }

        }
    }

    // Update is called once per frame
    private void Update()
    {
        // When the player crosses a certain point create new and delete the last tile
        if (playerTransform.position.z - safeZone > (spawnZ - tilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    // Function to spawn the tiles
    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go; // Game object

        // Spawn tiles
        if (prefabIndex == -1)
        {
            go = Instantiate(groundPrefabs[RandomPrefabIndex()]) as GameObject; 
        }
        else
        {
            go = Instantiate(groundPrefabs[prefabIndex]) as GameObject;
        }

        go.transform.SetParent(transform);                // Spawn new tiles as a child of TileMnager game object
        go.transform.position = Vector3.forward * spawnZ; // Spawn tiles forward on z
        spawnZ += tileLength;
        activeTiles.Add(go);                              // Contains the tiles currently on screen 
    }

    // Function to delete tiles
    private void DeleteTile()
    {
        Destroy(activeTiles[0]); // Destroys the object
        activeTiles.RemoveAt(0); // Removes it from the list
    }

    // Function to randomize the tiles
    private int RandomPrefabIndex()
    {
        if(groundPrefabs.Length <= 1 )
        {
            return 0;
        }

        int randomIndex = lastGroundIndex;
        while (randomIndex == lastGroundIndex)
        {
            randomIndex = Random.Range(1, groundPrefabs.Length);
        }

        lastGroundIndex = randomIndex;
        return randomIndex;
    }
}

