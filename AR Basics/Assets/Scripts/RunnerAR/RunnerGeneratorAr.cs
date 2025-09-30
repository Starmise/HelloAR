using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerGeneratorAr : MonoBehaviour
{
    [Header("Chunks")]
    public List<GameObject> chunksPrefabs;
    public Transform player;
    public int initialChunks = 2;
    public float spawnDistance = 49.5f;

    private List<GameObject> spawnedChunks = new List<GameObject>();
    private float zSpawn = 0f; // Current z position to spawn new chunks
    private float chunkLenght = 50;

    private void Start()
    {
        enabled = false;

        for (int i = 0; i < initialChunks; i++)
        {
            SpawnChunk(i == 0); // The first chunk will always be the same
        }
    }

    private void Update()
    {
        if (spawnedChunks.Count < initialChunks + 2)
        {
            SpawnChunk();
        }
    }

    void SpawnChunk(bool isInitial = false)
    {
        GameObject chunk;

        if (isInitial)
        {
            chunk = Instantiate(chunksPrefabs[0], transform);
            chunk.transform.localPosition = Vector3.forward * zSpawn;
        }
        else
        {
            int index = Random.Range(0, chunksPrefabs.Count);
            chunk = Instantiate(chunksPrefabs[index], transform);
            chunk.transform.localPosition = Vector3.forward * zSpawn;
        }

        spawnedChunks.Add(chunk);
        zSpawn += chunkLenght;
    }

    public void ReturToMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
