using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public bool isJump;
    private bool isFire;
    public float timeToExitAttack;
    public float movement;

    private Rigidbody2D rig;
    public Transform firePoint;
    public GameObject musicNote;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        SoulFire();
        animacione();
    }

    void Move()
    {
        movement = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if(movement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if(movement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isJump = true;
            }
        }
    }

    void SoulFire()
    {
        StartCoroutine("Fire");
    }

    void animacione()
    {
        if (isJump)
        {
            anim.SetInteger("transition", 2);
        }
        else
        {
            if(movement != 0)
            {
                anim.SetInteger("transition", 1);
            }
            else
            {
                anim.SetInteger("transition", 0);
            }
        }
    }

    IEnumerator Fire()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isFire == false)
            {
                isFire = true;
                anim.SetTrigger("attacking");
                GameObject bolaFogo = Instantiate(musicNote, firePoint.position, firePoint.rotation);
                Invoke(nameof(exitAttack), timeToExitAttack);

                if (transform.rotation.y == 0)
                {
                    bolaFogo.GetComponent<Notamusical>().isRight = true;
                }
                if (transform.rotation.x == 180)
                {
                    bolaFogo.GetComponent<Notamusical>().isRight = false;
                }

                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    void exitAttack()
    {
        isFire = false;
        anim.SetBool("isfiring", false);
    }
}
