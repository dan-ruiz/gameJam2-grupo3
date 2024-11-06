using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalkingBehaviour : StateMachineBehaviour
{
    private BossStats bossStats; // Referencia a BossStats
    private Rigidbody2D rb2D; // Referencia al Rigidbody2D
    [SerializeField] private float movementSpeed; // Velocidad de movimiento
    private Transform playerTransform; // Referencia al jugador
    private GhostAnimator ghostAnimator; // Referencia al GhostAnimator

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossStats = animator.GetComponent<BossStats>();
        rb2D = bossStats.rb2D; // Asegúrate de que esto esté correcto
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        ghostAnimator = animator.GetComponent<GhostAnimator>(); // Obtener referencia al GhostAnimator
        bossStats.LookAtPlayer();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerTransform != null)
        {
            // Calcula la dirección hacia el jugador
            Vector2 direction = (new Vector2(playerTransform.position.x, playerTransform.position.y) - rb2D.position).normalized;

            // Aplica el movimiento
            rb2D.velocity = direction * movementSpeed;

            // Actualiza las animaciones de movimiento
            // ghostAnimator.UpdateMovementAnimation(direction);

            // Para depuración
            Debug.Log("Boss Position: " + rb2D.position + " Direction: " + direction);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb2D.velocity = Vector2.zero; // Detén el movimiento al salir del estado
    }
}