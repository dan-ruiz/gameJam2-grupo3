using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage = 2; // Daño que causa al jugador
    public float lifespan = 5f; // Tiempo antes de destruirse automáticamente

    void Start()
    {
        Destroy(gameObject, lifespan); // Destruir la bola de fuego después de un tiempo
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si colisiona con el jugador
        if (collision.CompareTag("Player"))
        {
            // Lógica para causar daño al jugador
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Destruir la bola de fuego tras impactar al jugador
        }
        // Verificar si colisiona con cualquier otro objeto excepto el boss o otra bola de fuego
        else if (!collision.CompareTag("Boss") &&
            !collision.CompareTag("Fireball") &&
            !collision.CompareTag("MapConfiner") &&
            !collision.CompareTag("ChocolateCandy") &&
            !collision.CompareTag("YellowCandy") &&
            !collision.CompareTag("BlueCandy") &&
            !collision.CompareTag("RedCandy"))
        {
            Destroy(gameObject); // Destruir la bola de fuego si impacta con cualquier otro objeto
        }
    }
}
