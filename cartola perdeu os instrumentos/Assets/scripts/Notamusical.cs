using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Notamusical : MonoBehaviour
{
    private Rigidbody2D rig;
    public float speed;

    public int damage;

    public bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRight)
        {
            rig.velocity = Vector2.right * speed;
        }
        else
        {
            rig.velocity = Vector2.left * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.GetComponent<Enemy>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
