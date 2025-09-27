using System.Collections.Generic;
using UnityEngine;

public class RunnerGeneratorAr : MonoBehaviour
{
    [Header("Chunks")]
    public List<GameObject> chunksPrefabs;
    public Transform player;
    public int initialChunks = 2;
    public float spawnDistance = 50f;

    private List<GameObject> spawnedChunks = new List<GameObject>();
    private float zSpawn = 0f; // Current z position to spawn new chunks
    private float chunkLenght = 50;

    private void Start()
    {
        for (int i = 0; i < initialChunks; i++)
        {
            SpawnChunk(i == 0); // The first chunk will always be the same
        }
    }

    private void Update()
    {
        if (player.position.z > zSpawn - (initialChunks * chunkLenght))
        {
            SpawnChunk();
            DeleteChunk(); // The old one
        }
    }

    void SpawnChunk(bool isInitial = false)
    {
        GameObject chunk;

        if (isInitial)
        {
            chunk = Instantiate(chunksPrefabs[0], transform); // <-- parent = este objeto
            chunk.transform.localPosition = Vector3.forward * zSpawn;
        }
        else
        {
            int index = Random.Range(0, chunksPrefabs.Count);
            chunk = Instantiate(chunksPrefabs[index], transform); // <-- parent = este objeto
            chunk.transform.localPosition = Vector3.forward * zSpawn;
        }

        spawnedChunks.Add(chunk);
        zSpawn += chunkLenght;
    }

    void DeleteChunk()
    {
        if (spawnedChunks.Count > initialChunks + 2)
        {
            Destroy(spawnedChunks[0]);
            spawnedChunks.RemoveAt(0);
        }
    }
}
