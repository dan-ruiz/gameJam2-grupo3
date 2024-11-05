using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHealth; // Salud del jugador
    private GameManager gameManager;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Método para recibir daño
    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Aquí se puede agregar lógica para cuando el jugador muere, como reproducir una animación o sonido
        gameManager.GameOver();
    }
}
