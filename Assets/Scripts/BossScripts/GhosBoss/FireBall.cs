using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // Variables
    public float speed; // Asegúrate de que esta variable esté definida
    public float timer;
    public Transform initialPos;
    private void Start()
    {
        
    }
    private void Update()
    {
        // Calcula la dirección hacia la posición inicial
        Vector3 direction = (initialPos.position - transform.position).normalized;
        // Mueve el objeto en la dirección calculada
        transform.Translate(direction * speed * Time.deltaTime);
        // Calcula el ángulo en radianes
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Aplica la rotación al objeto
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        // Incrementa el timer
        timer += Time.deltaTime;
        // Desactiva el objeto después de 3 segundos
        if (timer > 3)
        {
            gameObject.SetActive(false);
            timer = 0;
        }
    }
}