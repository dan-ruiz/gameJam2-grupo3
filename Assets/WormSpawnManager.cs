using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSpawnManager : MonoBehaviour
{
    // Reference to the prefab to spawn
    public GameObject objectToSpawn;

    // Define the square range for random spawn locations
    public Vector2 spawnAreaMin = new Vector2(-12f, -9f);
    public Vector2 spawnAreaMax = new Vector2(12f, 7f);

    // Time interval between spawns
    public float spawnInterval = 1f;

    private float spawnTimer;
    private List<GameObject> objectPool = new List<GameObject>();
    private int poolSize = 40;

    void Start() {
        // Initialize the spawn timer
        spawnTimer = spawnInterval;

        // Initialize the object pool
        for (int i = 0; i < poolSize; i++) {
            GameObject obj = Instantiate(objectToSpawn);
            obj.SetActive(false); // Deactivate the object initially
            objectPool.Add(obj);
        }
    }

    void Update() {
        // Update timer
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f) {
            SpawnObject();
            spawnTimer = spawnInterval; // Reset timer
        }
    }

    void SpawnObject() {
        // Find an inactive object in the pool
        foreach (GameObject obj in objectPool) {
            if (!obj.activeInHierarchy) // Check if the object is inactive
            {
                // Generate a random position within the specified range
                float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
                float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
                Vector3 spawnPosition = new Vector3(randomX, randomY, 0); // Assuming z = 0 for ground level

                // Activate the object and set its position
                obj.transform.position = spawnPosition;
                obj.SetActive(true);
                return; // Exit the function after spawning an object
            }
        }
    }

    public void ReturnObjectToPool(GameObject obj) {
        obj.SetActive(false); // Deactivate the object to return it to the pool
    }
}

