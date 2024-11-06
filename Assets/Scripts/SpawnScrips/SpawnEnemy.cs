using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] spawns;
    [SerializeField] private GameObject enemy;

    // Tiempo de intervalo entre spawns
    public float spawnInterval = 15f; // Verificar si el tiempo esta bien asi o si debe ser mayor la espera
    // Cantidad máxima de enemigos a renderizar
    public int maxWavesOfEnemies = 4; // ??? Cual seria la cantidad de oleadas
    // Contador de enemigos instanciados
    private int enemiesSpawned = 0;


    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());

    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (enemiesSpawned < maxWavesOfEnemies)
        {
            foreach (var spawn in spawns)
            {
                // Instanciar el enemy en la posición del spawn
                GameObject enemy = SpawnEnemyPool.Instance.RequestEnemy();
                if (enemy != null)
                {
                    enemy.transform.position = spawn.position; // Colocar el enemigo en la posición del spawn
                    enemy.SetActive(true);
                }
            }
            enemiesSpawned++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
