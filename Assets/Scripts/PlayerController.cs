using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
//[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {
    NetworkedPlayer np;
    Animator playerAnim;

    [Header("Physics and Hitboxes")]
    [Space]
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .60f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = true;                          // Whether or not a player can move while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    //[SerializeField] private Collider2D m_CrouchDisableCollider;              // A collider that will be disabled when crouching
    private Rigidbody2D m_Rigidbody2D;
    public bool m_FacingRight = true;                                           // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    const float k_GroundedRadius = .2f;                                         // Radius of the overlap circle to determine if grounded
    const float k_CeilingRadius = .2f;                                          // Radius of the overlap circle to determine if the player can stand up
    public bool m_Grounded;
    // Whether or not the player is grounded.
    private float dazedTime;
    public float startDazedTime;

    [Header("Movement")]
    [Space]
    [SerializeField] private float runSpeed = 30f;
    [Range(0, 1)] [SerializeField] private float m_JumpAttackSpeed = .80f;      //movement speed multiplier for when jump attack
    public float horizontalMove = 0;
    private bool lockMovement = false;                                          //don't move when doing animation
    public bool jump = false;

    [Header("Attack")]
    [Space]
    [SerializeField] private float startTimeBtwAttack = 0.6f;
    [SerializeField] private float attackRange = 2f;
    private float timeBtwAttack;
    public float maxHealth = 100f;
    public float currentHealth = 80f;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public int damage;
    //public float attackRangeX; //for if you want rectangular hitbox
    //public float attackRangeY;

    [Header("Shoot")]
    [Space]
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Input Keys")]
    [Space]
    PlayerInputs input;
    public bool keyFire1 = false;
    public float keyHorizontal = 0;
    public bool keyJump = false;
    public bool keyCrouch = false;
    /*
    [Header("Camera")]
    [Space]
    [SerializeField] private Camera playerCamera;
    private Vector2 velocity;
    [SerializeField] private float smoothTimeY = 0;
    [SerializeField] private float smoothTimeX = 0;
    [SerializeField] private float moveCameraY = 0;
    [SerializeField] private float moveCameraX = 0;
    */

    private void Awake() {

    }
    private void Start() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent(typeof(Animator)) as Animator;
        np = GetComponent(typeof(NetworkedPlayer)) as NetworkedPlayer;
    }
    //Physics Updates
    private void FixedUpdate() {
        //Smooth Camera
        /*
        float posX = Mathf.SmoothDamp(playerCamera.transform.position.x, transform.position.x - moveCameraX, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(playerCamera.transform.position.y - moveCameraY, transform.position.y - moveCameraY, ref velocity.y, smoothTimeY);
        playerCamera.transform.position = new Vector3(posX, posY, transform.position.z);
        */

        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject) {
                m_Grounded = true;
                if (!wasGrounded && m_Rigidbody2D.velocity.y < 0)
                    playerAnim.SetBool("IsJumping", false);
                }
            }

        //m_Grounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 1.2f, transform.position.y - 1.2f), 
        //                                   new Vector2(transform.position.x + 1.2f, transform.position.y - 1.21f), 
        //                                   m_WhatIsGround);

        /*
        if (m_Grounded && m_Rigidbody2D.velocity.y < 0)
            playerAnim.Jump(false);
        */

        //Jumping
        if (m_Grounded && jump) {
            if(np.networkObject.IsOwner)
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            m_Grounded = false;
            playerAnim.SetBool("IsJumping", true);
        } else if (m_Grounded) {
            playerAnim.SetBool("IsJumping", false);
        } else {
            playerAnim.SetBool("IsJumping", true);
        }
        jump = false;

        //MOVEMENT
        if (m_Grounded || m_AirControl) {
            float move = horizontalMove * Time.fixedDeltaTime;
            if(np.networkObject.IsOwner) {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
                // If the input is moving the player right and the player is facing left...
            }
            if (move > 0 && !m_FacingRight) {
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight) {
                Flip();
            }
        }

    }

    void Update() {
        //ATTACK
        if (timeBtwAttack <= 0) {
            lockMovement = false; //allowed to move after delay
            if (keyFire1) {
                //horizontal speed control on attack
                lockMovement = true; //disable movement
                if (!m_Grounded) {
                    horizontalMove *= m_JumpAttackSpeed; //move 60% of speed if attack in air
                } else {
                    horizontalMove = 0;
                }
                playerAnim.SetTrigger("Attack");
                timeBtwAttack = startTimeBtwAttack;
            }
        } else {
            timeBtwAttack -= Time.deltaTime;
        }

        if(keyJump) {
            jump = true;
        }

        //PLAYER MOVEMENT
        if (keyCrouch && m_Grounded) {
            playerAnim.SetBool("IsCrouching", true);
            lockMovement = true; //no moving while crouching
            horizontalMove = 0;
        } else if (!keyCrouch) {
            playerAnim.SetBool("IsCrouching", false);
        }

        if (!lockMovement) {
            horizontalMove = keyHorizontal * runSpeed; 
            playerAnim.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }


    }

    private void Attack() {
        //HITBOX
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        //IF WANT RECTANGULAR USE: OverlapBoxAll(attack.position, new Vector2(attackRangeX, attackRangeY), 0); //change 0 for angle
        //loops through all enemies to deal damage, might change this.
        for (int i = 0; i < enemiesToDamage.Length; i++) {
            enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage(damage);
        }
    }

    private void Shoot() {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void TakeDamage(int damage) {
        dazedTime = startDazedTime;
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        currentHealth -= damage;
        //anim.SetTrigger("hit");
        if (m_FacingRight) {
            m_Rigidbody2D.AddForce(new Vector2(10f, 3f));
        } else {
            m_Rigidbody2D.AddForce(new Vector2(-10f, 3f));
        }
        
        if (currentHealth <= 0) {
            //anim.SetTrigger("death");
        }
    }

    private void Flip() {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - 1.2f),
            new Vector2(1, 0.01f));
    }

    public void updateInputs(float horizontal, bool fire1, bool jump, bool crouch) {
        keyFire1 = fire1;
        keyHorizontal = horizontal;
        keyJump = jump;
        keyCrouch = crouch;
    }
}