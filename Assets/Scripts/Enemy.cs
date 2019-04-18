using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    private float dazedTime;
    public float startDazedTime;

    private Animator anim;
    //public GameObject bloodEffect;
    private Rigidbody2D m_Rigidbody2D;

    public Animator enemyAnim;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(dazedTime <= 0)
        {
            speed = 2;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
       
    }

    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        //Debug.Log("damage Taken !");
        enemyAnim.SetTrigger("hit");
        m_Rigidbody2D.AddForce(new Vector2(7000f, 0));

    }
}
