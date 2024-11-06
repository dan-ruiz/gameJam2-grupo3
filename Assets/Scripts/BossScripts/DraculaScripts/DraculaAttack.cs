using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraculaAttack : MonoBehaviour
{
    public GameObject fireballPrefab; // Prefab de la bola de fuego
    public Transform fireballSpawnPoint; // Punto desde donde Drácula lanza la bola de fuego
    public float fireballSpeed = 5f; // Velocidad de la bola de fuego
    public float attackInterval = 2f; // Tiempo entre ataques
    private Animator animator; // Controlador de animaciones

    private Transform player; // Referencia al jugador
    private float attackCooldown; // Controlador de enfriamiento para el ataque

    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");

    void Start()
    {
        // Busca al jugador en la escena por su tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();
        attackCooldown = 0f;
    }

    void Update()
    {
        // Reduce el cooldown de ataque con el tiempo
        attackCooldown -= Time.deltaTime;

        if (player != null && attackCooldown <= 0f)
        {
            Attack();
            attackCooldown = attackInterval; // Reiniciar el cooldown
        }
    }

    void Attack()
    {
        // Activar animación de ataque
        animator.SetBool(IsAttackingHash, true);

        // Instanciar y lanzar la bola de fuego
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

        // Calcular la dirección hacia el jugador
        Vector2 direction = (player.position - fireballSpawnPoint.position).normalized;
        fireball.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed;

        // Desactivar animación de ataque después de un tiempo
        StartCoroutine(EndAttackAnimation());
    }

    private IEnumerator EndAttackAnimation()
    {
        yield return new WaitForSeconds(0.5f); // Duración de la animación de ataque
        animator.SetBool(IsAttackingHash, false);
    }

}