using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDetector : MonoBehaviour
{
    [Range(1, 15)]
    [SerializeField]
    private float viewRadius;
    public Transform viewCheckRange;
    public LayerMask targetLayer;
    public Vector2 initialPos;
    public bool targetInRange;

    private void Start() {
        initialPos = transform.position;
        targetLayer = GetComponent<FireController>().playerLayer;
    }
    private void Update() {
        targetInRange = Physics2D.OverlapCircle(viewCheckRange.position, viewRadius, targetLayer);
        if (viewCheckRange){

            // StartCoroutine();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(viewCheckRange.position, viewRadius);
    }
}
