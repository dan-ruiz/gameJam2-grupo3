using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireController : MonoBehaviour
{

    [Range(1, 15)]
    [SerializeField]
    private float viewRadius;
    public Transform fireCheckRange;
    public LayerMask playerLayer;
    public bool playerInRange;





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.Raycast(fireCheckRange.position, transform.right, viewRadius, playerLayer);
        if (playerInRange) { }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(fireCheckRange.position, viewRadius);
    }
}
