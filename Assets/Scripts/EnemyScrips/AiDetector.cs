using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDetector : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] float distance; 
    private Vector3 initialPosition;

   private Animator animator;
    private SpriteRenderer spriteRenderer;
    private void Start() {
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        distance = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("Distance",distance);
    }
    public void Rotate (Vector3 target){
        if (transform.position.x < target.x)
        {
            spriteRenderer.flipX = true;
        }
        else{
            spriteRenderer.flipY = false;
        }
    }
}
