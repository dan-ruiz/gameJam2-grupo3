using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GhostBoss : MonoBehaviour
{
    // Codigo Enemigo Base
    public int routine, hitSelect;
    public float timer, routineTime, grades, speed;
    public Animator anim;
    public Quaternion angle;
    public GameObject target;
    public bool attacking;
    public BossRange bossRange;
    public GameObject[] hit;

    // Lanzallamas Hechizo
    public bool flamethrowerSpell;
    public List<GameObject> spellPool = new List<GameObject>();
    public GameObject fire, head;
    public float fireTimer;
    // Dash 
    public float dashDistance;
    public bool directionSkill;

    // Bola de fuego 
    public GameObject fireBall, point;
    public List<GameObject> fireBallPool = new List<GameObject>();
    // 
    public int fase = 1;
    [SerializeField]private float maxHealthPoints;
    public float minHealthPoints;
    public Image lifeBar;
    // public AudioSource battleMusic;
    public bool death;

    // start
    private void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player");
        minHealthPoints = maxHealthPoints;

    }
    public void BossBehavioiur()
    {
        if (Vector2.Distance(transform.position, target.transform.position) < 10)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            lookPos.x = 0;
            point.transform.LookAt(target.transform.position);
            if (Vector2.Distance(transform.position, target.transform.position) > 1 && !attacking)
            {
                switch (routine)
                {
                    case 0:
                        // followPlayer
                        anim.SetBool("Movement", true);
                        anim.SetBool("Idle", false);
                        timer += 1 * Time.deltaTime;
                        if (timer > routineTime)
                        {
                            routine = Random.Range(0, 5);
                            timer = 0;
                            speed = 1;

                        }
                        break;
                    case 1:
                        // Run  
                        anim.SetBool("Movement", true);
                        anim.SetBool("Idle", false);
                        timer += 1 * Time.deltaTime;

                        if (Vector2.Distance(transform.position, target.transform.position) > 15)
                        {
                            WaitForSeconds(2);
                            routine = Random.Range(0, 5);
                            timer = 0;

                        }
                        break;
                    case 2:
                        anim.SetBool("Movement", false);
                        anim.SetBool("Idle", false);
                        anim.SetBool("ghost_Attack", true);
                        bossRange.GetComponent<CapsuleCollider2D>().enabled = false;
                        break;

                    case 3:
                        // Dash Attack
                        if (fase == 2)
                        {
                            dashDistance += 1 * Time.deltaTime;
                            anim.SetBool("Movement", false);
                            anim.SetBool("Idle", false);
                            anim.SetBool("ghost_Attack", false);
                            bossRange.GetComponent<CapsuleCollider2D>().enabled = false;
                            Vector2 direccion = (target.transform.position - transform.position).normalized;
                            transform.Translate(direccion * dashDistance);
                        }
                        else { routine = 0; timer = 0; }
                        break;
                    case 4:
                    if (fase ==2)
                    {
                        anim.SetBool("Movement",false);
                        anim.SetBool("Idle",false);
                        anim.SetBool("ghost_Attack",true);
                    }else { routine = 0; timer = 0; }
                    break;
                }
            }
        }
    }

    private void WaitForSeconds(int v)
    {
        throw new System.NotImplementedException();
    }
    public void FinalAnimation()
    {
        routine = 0;
        attacking = false;
        bossRange.GetComponent<CapsuleCollider2D>().enabled = false;
        flamethrowerSpell = false;
        dashDistance = 0;
        directionSkill = false;
    }
    public void AttackDirectionStart()
    {
        directionSkill = true;
    }
    public void AttackDirectionEnd()
    {
        directionSkill = false;
    }
    public void ColliderCandyTrue()
    {
        hit[hitSelect].GetComponent<CapsuleCollider2D>().enabled = true;
    }
    public void ColliderCandyFalse()
    {
        hit[hitSelect].GetComponent<CapsuleCollider2D>().enabled = false;
    }
    // Flamethrow Spell
    public GameObject GetSpell()
    {
        for (int i = 0; i < spellPool.Count; i++)
        {
            if (!spellPool[i].activeInHierarchy)
            {
                spellPool[i].SetActive(true);
                return spellPool[i];
            }
        }
        GameObject obj = Instantiate(fire, transform.position, Quaternion.identity) as GameObject;
        spellPool.Add(obj);
        return obj;
    }
    public void FlameThrowerSpell()
    {
        timer += 1 * Time.deltaTime;
        if (timer > 0.1f)
        {
            GameObject obj = GetSpell();
            obj.transform.position = transform.position;
            obj.transform.rotation = Quaternion.identity;
            fireTimer = 0;
        }
    }
    public GameObject GetFireball()
    {
        for (int i = 0; i < fireBallPool.Count; i++)
        {
            if (!fireBallPool[i].activeInHierarchy)
            {
                fireBallPool[i].SetActive(true);
                return fireBallPool[i];
            }
        }
        GameObject obj = Instantiate(fireBall, transform.position, Quaternion.identity) as GameObject;
        fireBallPool.Add(obj);
        return obj;
    }
    public void FireBallSpell()
    {
        GameObject obj = GetFireball();
        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.identity;
    }
    public void Alive(){
        if (minHealthPoints < 125)
        {
            fase = 2;
            routineTime =1;
        }
        BossBehavioiur();
        if (flamethrowerSpell) FlameThrowerSpell();
    }
    private void Update() {
        lifeBar.fillAmount = minHealthPoints / maxHealthPoints;
        if (minHealthPoints>0)
        {
             Alive();
        }else {
            if (!death) anim.SetTrigger("Death");
            death = true; 
        }
    }
}

