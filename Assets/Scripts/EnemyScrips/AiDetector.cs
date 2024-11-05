using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class AiDetector : MonoBehaviour
{
    [Range(1, 15)]
    [SerializeField]
    private float viewRadius;
    public Transform viewCheckRange;
    public LayerMask playerLayer;
    public bool targetInRange;
    private void Update()
    {
        CheckRange();
    }
    
    public void CheckRange(){
        targetInRange = Physics2D.OverlapCircle(viewCheckRange.position, viewRadius, playerLayer);
        if (targetInRange)
        {
            GetComponent<FollowPlayer>().FollowPlayerPosition();
        }else{
            GetComponent<AiPatrol>().Patrol();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(viewCheckRange.position, viewRadius);
    }
}