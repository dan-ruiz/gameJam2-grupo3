using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    [SerializeField] private float shootOffset;
    private Vector2 lastInput = Vector2.down;

    private GameManager gameManager;

    // Variables de animacion
    public bool hasShot = false;
    private PlayerAnimator playerAnimator;
    private float attackDuration = 0.1f; // Duración de la animación
    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");

    // Variables de Audio
    public AudioClip shootClip;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        inputHandler = FindObjectOfType<PlayerInputHandler>();
        if (inputHandler == null)
        {
            Debug.LogError($"No PlayerInputHandler found in scene for {gameObject.name}");
            enabled = false;
        }

        playerAnimator = GetComponentInChildren<PlayerAnimator>();
    }

    void Start()
    {

    }


    void LateUpdate()
    {
        if (gameManager.isGameActive)
        {
            Shoot();
        }


        if (inputHandler.MovementInput != Vector2.zero)
        {
            lastInput = inputHandler.MovementInput;
        }
    }

    public void Shoot()
    {
        if (inputHandler.IsAttackPressed)
        {
            hasShot = true;
            if (playerAnimator != null)
            {
                playerAnimator.GetComponent<Animator>().SetBool(IsAttackingHash, true);
            }
            StartCoroutine(ResetShootState());
            //Debug.Log("hasShot: " + hasShot);

            // Instanciar el candy en la posición del punto de disparo
            GameObject candy = CandyPool.Instance.RequestCandy();
            candy.transform.position = (Vector2)transform.position + shootOffset * inputHandler.MovementInput;
            candy.TryGetComponent(out Candy shootCandy);
            candy.SetActive(true);
            shootCandy?.SetDirection(inputHandler.MovementInput != Vector2.zero ? inputHandler.MovementInput : lastInput);

            AudioManager.Instance.PlaySFX(shootClip);

        }
        hasShot = false;
        //Debug.Log("2nd hasShot: " + hasShot);

    }
    private IEnumerator ResetShootState()
    {
        yield return new WaitForSeconds(attackDuration);
        hasShot = false;
        if (playerAnimator != null)
        {
            playerAnimator.GetComponent<Animator>().SetBool(IsAttackingHash, false);
        }
    }

}
