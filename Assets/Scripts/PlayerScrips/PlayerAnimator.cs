using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Animation handling
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;
    private PlayerCombat combat;

    // Cache animator parameters for better performance
    private readonly int HorizontalHash = Animator.StringToHash("MovementX");
    private readonly int VerticalHash = Animator.StringToHash("MovementY");

    private readonly int IdleHorizontalHash = Animator.StringToHash("LastX");
    private readonly int IdleVerticalHash = Animator.StringToHash("LastY");

    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");
    private readonly int IsDashingHash = Animator.StringToHash("IsDashing");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = transform.parent.gameObject.GetComponent<PlayerMovement>(); // esta es la que se integra
        //combat = transform.parent.gameObject.GetComponent<PlayerCombat>();

        // Subscribe to events
        movement.OnMove += HandleMovementAnimation;
        //combat.OnAttackStart += HandleAttackStart;
        //combat.OnAttackEnd += HandleAttackEnd;

        // Set a default facing direction (e.g., right)
        animator.SetFloat(IdleHorizontalHash, 0f);
        animator.SetFloat(IdleVerticalHash, -1f);
    }

    private void HandleMovementAnimation(Vector2 movement)
    {

        animator.SetFloat(HorizontalHash, movement.x);
        animator.SetFloat(VerticalHash, movement.y);

        if (movement.magnitude > 0)
        {
            animator.SetFloat(IdleHorizontalHash, movement.x);
            animator.SetFloat(IdleVerticalHash, movement.y);
        }
    }

    private void HandleAttackStart() => animator.SetBool(IsAttackingHash, true);
    private void HandleAttackEnd() => animator.SetBool(IsAttackingHash, false);

    private void OnDestroy()
    {
        // Unsubscribe from events
        if (movement != null)
        {
            movement.OnMove -= HandleMovementAnimation;
        }
        if (combat != null)
        {
            combat.OnAttackStart -= HandleAttackStart;
            combat.OnAttackEnd -= HandleAttackEnd;
        }
    }
}