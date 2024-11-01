using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("MovimientoJugador")]

    [SerializeField] private float playerSpeed;
    [SerializeField] private Vector2 direction;
    private Rigidbody2D playerRb2D;

    [Header("MovimientoCamara")]

    private Vector3 target;
    private Camera camera;

    // Variables relacionadas al disparo
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootSpeed = 5f;


    void Start()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }


    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        PlayerRotation();
        Shoot();
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        playerRb2D.MovePosition(playerRb2D.position + direction * playerSpeed * Time.fixedDeltaTime);
    }

    void PlayerRotation()
    {
        target = camera.ScreenToWorldPoint(Input.mousePosition);

        float radAngle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x);
        float gradesAngle = (180 / Mathf.PI) * radAngle - 90;
        transform.rotation = Quaternion.Euler(0, 0, gradesAngle);
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Obtener la posición del mouse en el mundo
            Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Asegurarse de que la posición z sea 0

            // Calcular la dirección del disparo
            Vector3 shootDirection = (mousePosition - shootPoint.position).normalized;

            // Instanciar el candy en la posición del punto de disparo
            GameObject candy = CandyPool.Instance.RequestCandy();
            candy.transform.position = shootPoint.position;
            candy.SetActive(true);

            // Aplicar la dirección del disparo al candy
            Candy candyScript = candy.GetComponent<Candy>();
            if (candyScript != null)
            {
                candyScript.SetDirection(shootDirection);
            }
        }
    }
}
