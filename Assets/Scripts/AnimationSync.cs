using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is responsible for *most* of the animations synced across the network.
/// </summary>
public class AnimationSync : MonoBehaviour {
    [SerializeField]
    //Get a refrence to the main network object script
    private NetworkedPlayer np;

    //The view model animator
    [SerializeField]
    private Animator animator;
    public PlayerController player;

    void Update() {
        //if we are the owner
        if (np.networkObject.IsOwner) {
            /*
            var horizontal = Input.GetAxis("Horizontal");

            //set the "IsMoving" variable on the world & view model
            if(horizontal > 0) {
                if (animator.gameObject.activeInHierarchy) {
                    animator.SetFloat("Speed", 30f);
                    //set the view model
                }
                //Set the bool across the network
                np.networkObject.Speed = 30f;
            } else if(horizontal < 0) {
                if (animator.gameObject.activeInHierarchy) {
                    player.transform.localScale = new Vector3(player.transform.localScale.x * -1, player.transform.localScale.y, player.transform.localScale.z);
                    animator.SetFloat("Speed", 30f);
                    //set the view model
                }
                //Set the bool across the network
                np.networkObject.Speed = 30f;
            } else {
                if (animator.gameObject.activeInHierarchy) {
                    animator.SetFloat("Speed", 0f);
                }
                np.networkObject.Speed = 0f;
            }
            */
            /*
            if (Input.GetButtonDown("Crouch")) {
                if (animator.gameObject.activeInHierarchy) {
                    animator.SetBool("isCrouching", true);
                }
                np.networkObject.isCrouching = true;
            } else if(Input.GetButtonUp("Crouch")) {
                if (animator.gameObject.activeInHierarchy) {
                    animator.SetBool("isCrouching", false);
                }
                np.networkObject.isCrouching = false;
            }
            */
            /*
            if (!player.m_Grounded) {
                if(animator.gameObject.activeInHierarchy) {
                    animator.SetBool("isJumping", true);
                }
                np.networkObject.isJumping = true;
            } else if (!player.m_Grounded) {
                if (animator.gameObject.activeInHierarchy) {
                    animator.SetBool("isJumping", false);
                }
                np.networkObject.isJumping = false;
            }
            */
            /*
            if (!player.wasGrounded && m_Rigidbody2D.velocity.y < 0)
                animator.SetBool("IsJumping", false);
            */
        } else {
            if (animator.gameObject.activeInHierarchy) {
                //Use the values set by the owner
                //animator.SetFloat("Speed", np.networkObject.Speed);
                //animator.SetBool("isCrouching", np.networkObject.isCrouching);
                //animator.SetBool("isJumping", np.networkObject.isJumping);
            }
        }
    }
}
