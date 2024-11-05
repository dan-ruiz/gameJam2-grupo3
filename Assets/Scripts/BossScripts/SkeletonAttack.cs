using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    //[SerializeField] private float shootOffset;
    //private Vector2 lastInput = Vector2.down;

    public bool hasShot = false;
    private SkeletonAnimator skeletonAnimator;
    private float attackDuration = 0.1f; // Duración de la animación
    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");


    void Awake() {
        inputHandler = FindObjectOfType<PlayerInputHandler>();
        if (inputHandler == null) {
            Debug.LogError($"No PlayerInputHandler found in scene for {gameObject.name}");
            enabled = false;
        }

        skeletonAnimator = GetComponentInChildren<SkeletonAnimator>();
    }

    void Start() {

    }


    void LateUpdate() {
        Shoot();
        /*
        if (inputHandler.MovementInput != Vector2.zero) {
            lastInput = inputHandler.MovementInput;
        }
        */
    }

    public void Shoot() {
        if (inputHandler.IsAttackPressed) {
            hasShot = true;
            if (skeletonAnimator != null) {
                skeletonAnimator.GetComponent<Animator>().SetBool(IsAttackingHash, true);
            }
            StartCoroutine(ResetShootState());

            /*
            // Instanciar el candy en la posición del punto de disparo
            GameObject candy = CandyPool.Instance.RequestCandy();
            candy.transform.position = (Vector2)transform.position + shootOffset * inputHandler.MovementInput;
            candy.TryGetComponent(out Candy shootCandy);
            candy.SetActive(true);
            shootCandy?.SetDirection(inputHandler.MovementInput != Vector2.zero ? inputHandler.MovementInput : lastInput);
            */

        }
        hasShot = false;

    }
    private IEnumerator ResetShootState() {
        yield return new WaitForSeconds(attackDuration);
        hasShot = false;
        if (skeletonAnimator != null) {
            skeletonAnimator.GetComponent<Animator>().SetBool(IsAttackingHash, false);
        }
    }

}