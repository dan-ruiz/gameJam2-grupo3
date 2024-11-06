using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class SkeletonBossStats : MonoBehaviour
{
    public Animator animator; // Referencia al Animator
    public Rigidbody2D rb2D; // Referencia al Rigidbody2D
    public Transform player; // Referencia al jugador

    // Variables de estado
    public float lifePoints, maxLifePoints, speed, meleeDamage, spellDamage;
    private bool isFacingRight = true; // Indica si el boss está mirando a la derecha
    private bool isFacingUp = true; // Indica si el boss está mirando hacia arriba

    // Variables para el umbral de dirección
    [SerializeField] private float directionThreshold = 0.1f; // Umbral para determinar cuando cambiar dirección

    // ACÁ IRÍA LO DE LA BARRA DE LA VIDA

    [Header("Ataque")]
    [SerializeField] private Transform attackController;
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackDamage;

    // Nueva enum para tracking de dirección
    private enum FacingDirection {
        Up,
        Down,
        Left,
        Right
    }
    private FacingDirection currentDirection = FacingDirection.Down;
    private float attackThreshold = 3f; // Distancia a la que el boss atacará
    private bool canAttack = true;      // Para controlar el cooldown del ataque

    void Start() {
        // Definición de variables del boss
        animator = GetComponent<Animator>();
        lifePoints = maxLifePoints;
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        animator.SetBool("IsMovingHorizontal", true);

    }
    private void Update() {
        float playerDistance = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("PlayerDistance", playerDistance);

        // Actualizar la dirección
        LookAtPlayer();

        // Verificar si debe atacar
        if (playerDistance < attackThreshold && canAttack) {
            StartCoroutine(PerformAttack());
        }
    }

    public void TakeDamage(int amount) {
        lifePoints -= amount;

        if (lifePoints <= 0) {
            //animator.SetBool("IsDeath", true);
            animator.SetTrigger("Death");
        }
    }

    private void Death() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            // Intenta obtener el componente PlayerHealth
            if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth)) {
                // Aplica daño al jugador
                playerHealth.TakeDamage((int)meleeDamage);
            }
        } else if (collision.TryGetComponent<Candy>(out _)) // Usamos '_' para indicar que no usamos la variable
          {
            int damageAmount = CheckCandy(collision.gameObject);
            if (damageAmount > 0) {
                //TakeDamage(damageAmount);
            }
        }
    }

    int CheckCandy(GameObject candy) {
        return candy.tag switch {
            "ChocolateCandy" => 5, // Daño específico para ChocolateCandy
            "YellowCandy" => 10,    // Daño específico para YellowCandy
            "BlueCandy" => 15,       // Daño específico para BlueCandy
            "RedCandy" => 20,        // Daño específico para RedCandy
            _ => 0,                  // Si no es un tipo de candy esperado, no aplica daño
        };
    }

    public void LookAtPlayer() {
        Vector2 direction = (player.position - transform.position).normalized;
        float absX = Mathf.Abs(direction.x);
        float absY = Mathf.Abs(direction.y);
        float hysteresis = 0.1f;

        // Determine predominant direction with hysteresis
        bool isHorizontalMovement =
            (animator.GetBool("IsMovingHorizontal") && absX > absY - hysteresis) ||
            (!animator.GetBool("IsMovingHorizontal") && absX > absY + hysteresis);

        // Update animator parameters
        animator.SetFloat("HorizontalDirection", direction.x);
        animator.SetFloat("VerticalDirection", direction.y);

        if (isHorizontalMovement) {
            animator.SetBool("IsMovingHorizontal", true);
            animator.SetBool("IsMovingVertical", false);

            // IMPORTANT: Update direction before flipping the sprite
            if (direction.x > directionThreshold) {
                currentDirection = FacingDirection.Right;
                if (!isFacingRight) {
                    isFacingRight = true;
                    transform.localScale = new Vector3(1, 1, 1);
                }
            } else if (direction.x < -directionThreshold) {
                currentDirection = FacingDirection.Left;
                if (isFacingRight) {
                    isFacingRight = false;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        } else {
            animator.SetBool("IsMovingHorizontal", false);
            animator.SetBool("IsMovingVertical", true);

            if (direction.y > directionThreshold) {
                currentDirection = FacingDirection.Up;
            } else if (direction.y < -directionThreshold) {
                currentDirection = FacingDirection.Down;
            }
        }

        // Store the last valid direction when movement is significant enough
        if (absX > directionThreshold || absY > directionThreshold) {
            lastValidDirection = currentDirection;
        }
    }

    private IEnumerator PerformAttack() {
        canAttack = false;

        // Use the last valid direction for attack animation
        string attackTrigger = lastValidDirection switch {
            FacingDirection.Up => "AttackUp",
            FacingDirection.Down => "AttackDown",
            FacingDirection.Left => "AttackLeft",
            FacingDirection.Right => "AttackRight",
            _ => GetNearestDirectionalAttack()
        };

        // Debug log to verify attack trigger
        Debug.Log($"Triggering attack: {attackTrigger}");
        animator.SetTrigger(attackTrigger);

        // Perform the attack
        Attack();

        // Wait for animation to complete
        yield return new WaitForSeconds(1f);

        // Attack cooldown
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }


    public void Attack() {
        Collider2D[] objects = Physics2D.OverlapCircleAll(attackController.position, attackRadius);
        foreach (Collider2D colision in objects) {
            if (colision.CompareTag("Player")) {
                colision.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }
        }
    }

    private string GetNearestDirectionalAttack() {
        Vector2 direction = (player.position - transform.position).normalized;

        // Calculate angles to each direction
        float upAngle = Vector2.Angle(Vector2.up, direction);
        float downAngle = Vector2.Angle(Vector2.down, direction);
        float leftAngle = Vector2.Angle(Vector2.left, direction);
        float rightAngle = Vector2.Angle(Vector2.right, direction);

        float minAngle = Mathf.Min(upAngle, downAngle, leftAngle, rightAngle);

        // Add debug logging
        string chosenDirection = minAngle switch {
            var a when a == upAngle => "AttackUp",
            var a when a == downAngle => "AttackDown",
            var a when a == leftAngle => "AttackLeft",
            _ => "AttackRight"
        };
        Debug.Log($"Nearest attack direction: {chosenDirection} (angles: up={upAngle}, down={downAngle}, left={leftAngle}, right={rightAngle})");

        return chosenDirection;
    }

    // Add this method to help with debugging
    void OnValidate() {
        // Ensure attack radius is visible in editor
        if (attackRadius <= 0) {
            attackRadius = 1f;
            Debug.LogWarning("Attack radius was set to a minimum of 1 unit");
        }
    }

    private FacingDirection lastValidDirection = FacingDirection.Down;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackController.position, attackRadius);
    }
}