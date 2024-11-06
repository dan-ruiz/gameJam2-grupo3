using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerSpell : MonoBehaviour
{
    private float timer;
    private float speed = 6f;
    private Vector2 InitialPos;
    private void Start()
    {
        InitialPos = new Vector2(transform.position.x, transform.position.y);
    }
    private void Update()
    {
        transform.Translate(InitialPos * speed * Time.deltaTime);
        transform.localScale += new Vector3(2, 2,0) * Time.deltaTime;

        timer += 1 * Time.deltaTime;
        if (timer > 1)
        {
            transform.localScale = new Vector2(1, 1);
            gameObject.SetActive(false);
            timer = 0;
        }
    }
}