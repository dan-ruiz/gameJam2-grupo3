using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // variables
    public float timer;
    public Transform initialPos;
    private void Start()
    {

    }
    private void Update()
    {
        
        timer += 1 * Time.deltaTime;
        if (timer > 3)
        {
            gameObject.SetActive(false);
            timer = 0;
        }
        transform.Translate(InitialPos * speed * Time.deltaTime);
    }
}