using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{

    [SerializeField] protected float candySpeed = 6f;
    [SerializeField] protected Rigidbody2D candyRb;
    [SerializeField] protected int damage = 5;
    [SerializeField] protected float maxDistance = 10f; // Distancia máxima que puede recorrer el candy
    protected Vector2 startPosition;
    protected CandyPool candyPool;
    private static readonly HashSet<string> ignoreTags = new HashSet<string> { "Player", "Ally", "Candy", "ChocolateCandy", "YellowCandy", "BlueCandy", "RedCandy", "MapConfiner" };
    
    private void Update()
    {
        ShootDistance();
    }

    private void OnEnable()
    {
        startPosition = transform.position;
        // Asegurar que tengamos una referencia al pool cuando el objeto se activa
        if (candyPool == null) {
            candyPool = CandyPool.Instance;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto con el que colisiona no tiene el tag "Player", "Ally" o "Candy"
        if (!ignoreTags.Contains(collision.gameObject.tag))
        {
            if (collision.TryGetComponent<Enemy>(out var enemy))enemy.TakeDamage(damage);
            //else if (collision.TryGetComponent<BossStats>(out var boss)) boss.TakeDamage(damage);
            else if (collision.TryGetComponent<SkeletonBossStats>(out var skeletonBoss)) skeletonBoss.TakeDamage(damage);
            else if (collision.TryGetComponent<BossStats>(out var boss)) boss.TakeDamage(damage);
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

    /*
    private void ReturnToPool()
    {
        if (candyPool != null)
        {
            candyPool.ReturnCandyToPool(gameObject);
        }
        else
        {
            Debug.LogError("CandyPool is not set for this candy object.");
        }
    }
    */

    private void ReturnToPool() {
        // Si no tenemos referencia al pool, intentar obtenerla del singleton
        if (candyPool == null) {
            candyPool = CandyPool.Instance;
            if (candyPool == null) {
                Debug.LogError("CandyPool instance not found in scene.");
                return;
            }
        }
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
