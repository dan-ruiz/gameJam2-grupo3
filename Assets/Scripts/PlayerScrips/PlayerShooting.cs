using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private float shootOffset;
    private Vector2 lastInput = Vector2.down;

    void Start()
    {

    }


    void LateUpdate()
    {
        Shoot();

        if (inputHandler.MovementInput != Vector2.zero)
        {
            lastInput = inputHandler.MovementInput;
        }
    }

    public void Shoot()
    {
        if (inputHandler.IsAttackPressed)
        {
            // Instanciar el candy en la posición del punto de disparo
            GameObject candy = CandyPool.Instance.RequestCandy();
            candy.transform.position = (Vector2)transform.position + shootOffset * inputHandler.MovementInput;
            candy.TryGetComponent(out Candy shootCandy);
            candy.SetActive(true);
            shootCandy?.SetDirection(inputHandler.MovementInput != Vector2.zero ? inputHandler.MovementInput : lastInput);


        }
    }
}
