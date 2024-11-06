using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoss : MonoBehaviour
{
    // Variables
    private Animator animator;
    public Rigidbody2D rigidB2D;
    public Transform player; 
    private bool checkPlayerSide;
    [Header ("LifePoint")]
    [SerializeField] private float lifePoints;
    // [SerializeField] private LifeBar lifeBar;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidB2D = GetComponent<Rigidbody2D>();
        // lifeBar.IniciateLifeBar(lifePoints);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        lifePoints -= damage;
        // lifeBar.ChangeLifePoints(lifePoints);
        if (lifePoints <= 0)
        {
            animator.SetTrigger("Death");
        }
    } 
    private void Muerte(){
        Destroy(gameObject);
    }
    private void LookPlayerSide(){
        if ((player.position.x> transform.position.x && !checkPlayerSide) || (player.position.x < transform.position.x && checkPlayerSide)) 
        {
            checkPlayerSide = !checkPlayerSide;
            transform.eulerAngles = new Vector2( 0 ,transform.eulerAngles.y + 180);
        }
    }
}
