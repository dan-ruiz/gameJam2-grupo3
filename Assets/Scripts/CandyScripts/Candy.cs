using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{

    [SerializeField] private float candySpeed = 6f;
    [SerializeField] private Rigidbody2D candyRb;
    [SerializeField] private int damage = 5;
    [SerializeField] private float maxDistance = 10f; // Distancia máxima que puede recorrer el candy
    private Vector2 startPosition;
    private CandyPool candyPool;


    private void Update()
    {
        ShootDistance();
    }

    private void OnEnable()
    {
        startPosition = transform.position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto con el que colisiona no tiene el tag "Player", "Ally" o "Candy"
        if (!collision.gameObject.CompareTag("Player") &&
            !collision.gameObject.CompareTag("Ally") &&
            !collision.gameObject.CompareTag("Candy") &&
            !collision.gameObject.CompareTag("MapConfiner")) // Este colicionador es el del cinemachine para que no genere problemas al hacer Shot()
        {
            // Aquí se debe agregar lógica adicional si es necesario, como reducir la vida del enemigo
            /*
            if (collision.gameObject.CompareTag("Enemy"))
            {
                // Obtener el componente Enemy del objeto colisionado
                collision.GetComponent<Enemy>().TakeDamage(damage);
            }
            */

            ReturnToPool();
        }
    }

    public void SetPool(CandyPool candyPool)
    {
        this.candyPool = candyPool;
    }

    public void SetDirection(Vector2 direction)
    {
        candyRb.velocity = direction * candySpeed;
    }

    private void ReturnToPool()
    {
        candyPool.ReturnCandyToPool(gameObject);
    }

    private void ShootDistance()
    {
        // Calcular la distancia recorrida
        float distanceTravelled = Vector2.Distance(startPosition, transform.position);
        if (distanceTravelled >= maxDistance)
        {
            ReturnToPool();
        }
    }

}
