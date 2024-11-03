using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    // Variables
    public int healthPoints;

    public void TakeDamage(int damage){
        healthPoints-= damage;
        if (healthPoints<=0)
        {
            
        }
    }
}
