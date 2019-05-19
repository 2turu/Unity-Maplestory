using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public float velX = 20f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * velX;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        EnemyController enemy = hitInfo.GetComponent<EnemyController>();
        if(enemy != null) {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
