using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour {
    NetworkedEnemy ne;
    private Animator anim;
    private Rigidbody2D m_Rigidbody2D;

    [Header("Status")]
    bool isDead;
    public int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private float speed;
    private float dazedTime;
    public float startDazedTime;
    private Transform target;

    [Header("Physics")]
    [SerializeField] private float knockbackX = 0;
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .60f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f;         // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;                     // Whether or not the player is grounded.
    private bool m_FacingRight = true;          // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private Vector2 jumpForce;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    void Start() {
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (dazedTime <= 0) {
            speed = 2;
        } else {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        //move test
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
    }

    void FixedUpdate() {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject) {
                m_Grounded = true;
                if (!wasGrounded && m_Rigidbody2D.velocity.y < 0)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void TakeDamage(int damage) {
        if (!isDead) {
            dazedTime = startDazedTime;
            //Instantiate(bloodEffect, transform.position, Quaternion.identity);
            currentHealth -= damage;
            anim.SetTrigger("hit");
            m_Rigidbody2D.AddForce(new Vector2(knockbackX, 0));
            if (currentHealth <= 0) {
                isDead = true;
                anim.SetTrigger("death");
            }
        }
    }

    public void Move(float move, bool crouch, bool jump) {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded) {

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            //networkObject.position = transform.position;

            //Flip sprite based on movement
            if (move > 0 && !m_FacingRight) {
                Flip();
            } else if (move < 0 && m_FacingRight) {
                Flip();
            }
        }

        //jump
        if (m_Grounded && jump) {
            //vertical force to the player.
            jumpForce = new Vector2(0f, m_JumpForce);
            m_Rigidbody2D.AddForce(jumpForce);
            m_Grounded = false;
        }
    }

    private void Flip() {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        //networkObject.localScale = transform.localScale;
    }

    public void Die() {
        Destroy(gameObject);
    }

    public int getMaxHealth() {
        return maxHealth;
    }

    public int getCurrentHealth() {
        return currentHealth;
    }
}