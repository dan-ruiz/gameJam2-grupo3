using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRange
{
    public Animator anim;
    public GhostBoss ghostBoss;
    public int melee;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            melee = Random.Range(0, 4);
            switch (melee)
            {
                case 0:
                    // Hit 1
                    anim.SetFloat("skills", 0);
                    ghostBoss.hitSelect = 0;
                    break;
                case 1:
                    anim.SetFloat("skills", 0);
                    ghostBoss.hitSelect = 1;
                    break;
                case 2:
                    anim.SetFloat("skills", 0);
                    ghostBoss.hitSelect = 2;
                    break;
                case 3:
                    // Fire Ball
                    if (ghostBoss.fase == 2) anim.SetFloat("skill", 0);
                    else {melee=0;}
                    break;  
            }
            anim.SetBool("movement", false);
            anim.SetBool("Idle", false);
            ghostBoss.attacking = true;
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
}