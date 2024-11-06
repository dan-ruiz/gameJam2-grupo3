using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    private WormSpawnManager spawnManager;
    private Enemy enemy;

    void Start() {
        // Find the SpawnManager in the scene
        spawnManager = FindObjectOfType<WormSpawnManager>();
        enemy = FindObjectOfType<Enemy>();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger detected with: " + other.name); // Log the name of the colliding object

        if (other.CompareTag("Player")) // Make sure your player has the "Player" tag
        {
            Debug.Log("Trigger Hit Player");
            // Return this object to the pool
            spawnManager.ReturnObjectToPool(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Worm collided with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("Hit Player");
            spawnManager.ReturnObjectToPool(gameObject);
        }
    }
}