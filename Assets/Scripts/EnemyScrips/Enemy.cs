using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int enemyHealth; // Salud del enemigo
    [SerializeField] private int initialHealth; // Salud inicial del enemigo
    [SerializeField] private int damage; // Daño que el enemigo puede infligir
    [SerializeField] private float speed; // Velocidad de movimiento del enemigo

    private SpawnEnemyPool enemyPool;

    void Start()
    {
        initialHealth = enemyHealth; // Guardar la salud inicial
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        enemyHealth = initialHealth; // Restablecer la salud cuando el enemigo se active
    }

    // Método para recibir daño
    public void TakeDamage(int amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Aquí se puede agregar lógica para cuando el enemigo muere, como reproducir una animación o sonido
        enemyPool.ReturnEnemyToPool(gameObject); // Desactivar el enemigo del pool
    }

    // Método para establecer el pool de enemigos
    public void SetPool(SpawnEnemyPool pool)
    {
        enemyPool = pool;
    }


    // Método para infligir daño (opcional)
    /*public void Attack(Player player)
    {
        player.TakeDamage(damage);
    }*/
}
