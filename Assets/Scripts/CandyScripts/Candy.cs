using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{

    [SerializeField] private float candySpeed = 5f;
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
        // Verificar si el objeto con el que colisiona tiene el tag "Enemy"
        /*
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Aquí se debe agregar lógica adicional si es necesario, como reducir la vida del enemigo

            
            Obtener el componente Enemy del objeto colisionado
            other.GetComponent<Enemy>().TakeDamage(damage);
            

            ReturnToPool();
        }*/

        ReturnToPool();
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
