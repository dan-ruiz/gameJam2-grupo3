using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : MonoBehaviour {

    private PlayerInputHandler inputHandler;
    [SerializeField] private float shootOffset;
    private Vector2 lastInput = Vector2.down;

    public bool hasShot = false;
    private PlayerAnimator playerAnimator;
    private GameObject ghostAttack;
    private const string ghostAttackName = "GhostAttack";
    private SpriteRenderer ghostAttackRenderer;

    private float attackDuration = 0.5f; // Duración de la animación
    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");


    void Awake() {
        
        inputHandler = FindObjectOfType<PlayerInputHandler>();
        if (inputHandler == null) {
            Debug.LogError($"No PlayerInputHandler found in scene for {gameObject.name}");
            enabled = false;
        }

        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        ghostAttack = transform.Find(ghostAttackName).gameObject;

        // Get the SpriteRenderer from the ghost attack object
        ghostAttackRenderer = ghostAttack.GetComponent<SpriteRenderer>();
    }

    void Start() {

    }


    void LateUpdate() {
        Shoot();
        
        if (inputHandler.MovementInput != Vector2.zero) {
            lastInput = inputHandler.MovementInput;

            // Adjust the sorting order based on vertical movement
            if (ghostAttackRenderer != null) {
                // If moving up, render behind the player
                if (lastInput.y > 0) {
                    ghostAttackRenderer.sortingOrder = -1; // Or any value less than the player's sorting order
                } else {
                    ghostAttackRenderer.sortingOrder = 1; // Or any value greater than the player's sorting order
                }
            }
        }
        
    }

    public void Shoot() {
        if (inputHandler.IsAttackPressed) {
            hasShot = true;
            ghostAttack.SetActive(hasShot);  //true

            StartCoroutine(ResetShootState());

        }
        hasShot = false;

    }
    private IEnumerator ResetShootState() {
        yield return new WaitForSeconds(attackDuration);
        hasShot = false;
        ghostAttack.SetActive(hasShot);  //false
    }

}
