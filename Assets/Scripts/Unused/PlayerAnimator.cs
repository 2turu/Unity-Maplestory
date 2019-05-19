using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    //private NetworkedPlayer np;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump(bool jump) {
        anim.SetBool("IsJumping", jump);
    }

    public void Attack() {
        anim.SetTrigger("Attack");
    }

    public void Crouch(bool crouch) {
        anim.SetBool("IsCrouching", crouch);
    }

    public void Move(float move) {
        anim.SetFloat("Speed", move);
    }
}
