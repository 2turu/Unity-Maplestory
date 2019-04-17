using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Game Manager")]
    public GameManager gameManager;

    [Header("Player Movement")]
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0;
    bool jump = false; //default not jumping
    bool crouch = false;
    private bool lockMovement = false; //don't move when doing animation

    [Header("Player Attack")]
    
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    //public Animator camAnim;
    public Animator playerAnim;
    public float attackRange;
    //public float attackRangeX; //for if you want rectangular hitbox
    //public float attackRangeY; //for if you want rectangular hitbox
    public int damage;
    [Range(0, 1)] [SerializeField] private float m_JumpAttackSpeed = .40f; //movement speed multiplier for when jumping
    private float timeBtwAttack;



    // Start is called before the first frame update
    /*
    void Start()
    {
        
    }
    */

    // Update is called once per frame
    void Update()
    {
        //stop all movement on chat focus
        if (!gameManager.chatBox.isFocused)
        {
            //PLAYER ATTACK
            Debug.Log(timeBtwAttack);
            if (timeBtwAttack <= 0)
            {
                lockMovement = false; //allowed to move after delay
                if (Input.GetKey(KeyCode.Z))
                {
                    //horizontal speed control on attack
                    lockMovement = true; //stop moving to attack
                    if (playerAnim.GetBool("IsJumping"))
                    {
                        horizontalMove *= m_JumpAttackSpeed; //move 60% of speed if attack in air
                    }
                    else
                    {
                        horizontalMove = 0;
                    }

                    //camAnim.SetTrigger("shake");
                    playerAnim.SetTrigger("Attack");
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange);
                    //IF WANT RECTANGULAR USE: OverlapBoxAll(attack.position, new Vector2(attackRangeX, attackRangeY), 0); //change 0 for angle
                    /*
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    }
                    */
                    timeBtwAttack = startTimeBtwAttack;
                }

            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }

            //PLAYER MOVEMENT
            if (!lockMovement)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            }

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                //animator.SetBool("IsJumping", true);
            }
            //FIX THIS JUMP
            if (controller.m_Grounded)
            {
                animator.SetBool("IsJumping", false);
            }
            else
            {
                animator.SetBool("IsJumping", true);
            }
            if (Input.GetButtonDown("Crouch") && controller.m_Grounded)
            {
                crouch = true;
                animator.SetBool("IsJumping", false);
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        //Time.fixedDeltaTime will be consistent
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
