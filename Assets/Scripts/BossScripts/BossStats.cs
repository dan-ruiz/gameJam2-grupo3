using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    public Animator animator; // Referencia al Animator
    public Rigidbody2D rb2D; // Referencia al Rigidbody2D
    private Transform player; // Referencia al jugador

    // Variables de estado
    public float lifePoints, maxLifePoints, speed, meleeDamage, spellDamage;
    private bool isFacingRight; // Indica si el boss está mirando a la derecha

    void Start()
    {
        // Definición de variables del boss
        lifePoints = maxLifePoints;
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void TakeDamage(int amount)
    {
        lifePoints -= amount;
        if (lifePoints <= 0)
        {
            animator.SetBool("IsDeath", true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Intenta obtener el componente PlayerHealth
            if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            {
                // Aplica daño al jugador
                playerHealth.TakeDamage((int)meleeDamage);
            }
        }
        else if (collision.TryGetComponent<Candy>(out _)) // Usamos '_' para indicar que no usamos la variable
        {
            int damageAmount = CheckCandy(collision.gameObject);
            if (damageAmount > 0)
            {
                TakeDamage(damageAmount);
            }
        }
    }

    int CheckCandy(GameObject candy)
    {
        return candy.tag switch
        {
            "ChocolateCandy" => 5, // Daño específico para ChocolateCandy
            "YellowCandy" => 10,    // Daño específico para YellowCandy
            "BlueCandy" => 15,       // Daño específico para BlueCandy
            "RedCandy" => 20,        // Daño específico para RedCandy
            _ => 0,                  // Si no es un tipo de candy esperado, no aplica daño
        };
    }

    public void LookAtPlayer()
    {
        if ((player.position.x > transform.position.x && !isFacingRight) || (player.position.x < transform.position.x && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            transform.eulerAngles = new Vector2(0, transform.eulerAngles.y + 180);
        }
    }
}