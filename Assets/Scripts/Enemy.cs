using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float currentHealth;
    public float maxHealth;
    [SerializeField] private float speed;
    private float dazedTime;
    public float startDazedTime;
    [SerializeField] private float knockbackX = 0;

    private Animator anim;
    private Rigidbody2D m_Rigidbody2D;

    public Animator enemyAnim;


    private void Awake() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        //anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update() {
        if (dazedTime <= 0) {
            speed = 2;
        } else {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }


        //move
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage) {
        dazedTime = startDazedTime;
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        currentHealth -= damage;
        enemyAnim.SetTrigger("hit");
        m_Rigidbody2D.AddForce(new Vector2(knockbackX, 0));
        if (currentHealth <= 0) {
            enemyAnim.SetTrigger("death");
        }

    }

    public void Die() {
        Destroy(gameObject);
    }
}
