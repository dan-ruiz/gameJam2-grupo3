using UnityEngine;

public class AiMovementGhost : MonoBehaviour
{
    public float speed = 2.0f;
    public float detectionRange = 5.0f;

    private Transform player;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= detectionRange)
            {
                animator.SetBool("isMoving", true);

                Vector2 direction = (player.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                // Actualiza los parámetros MovementX y MovementY en el Animator
                animator.SetFloat("MovementX", direction.x);
                animator.SetFloat("MovementY", direction.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }
}