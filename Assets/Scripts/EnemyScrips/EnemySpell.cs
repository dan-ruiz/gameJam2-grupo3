using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpell : MonoBehaviour
{
    // Variables
    public float speed, delayFire ;
    public int damage;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed );
    }
    public void setDirection(Vector2 dir)
    {
        direction = dir;

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthPlayer healtPoints))
        {
            healtPoints.TakeDamage(damage);
        }
    }
}
