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
            var horizontal = Input.GetAxis("Horizontal");

            //set the "IsMoving" variable on the world & view model
            if (horizontal != 0) {
                if (animator.gameObject.activeInHierarchy) {
                    animator.SetFloat("Speed", 30f);
                    //set the view model
                }
                //Set the bool across the network
                np.networkObject.Speed = 30f;
            } else //we aren't moving
              {
                if (animator.gameObject.activeInHierarchy) {
                    animator.SetFloat("Speed", 0f);
                }
                np.networkObject.Speed = 0f;
            }

            if (!player.m_Grounded) {
                if(animator.gameObject.activeInHierarchy) {
                    animator.SetBool("isJumping", true);
                }
                np.networkObject.isJumping = true;
            } else {
                if (animator.gameObject.activeInHierarchy) {
                    animator.SetBool("isJumping", false);
                }
                np.networkObject.isJumping = false;
            }

        } else //if we aren't the owner
          {
            if (animator.gameObject.activeInHierarchy) {
                //Use the values set by the owner
                animator.SetFloat("Speed", np.networkObject.Speed);
                animator.SetBool("isJumping", np.networkObject.isJumping);
            }

        }
    }
}
