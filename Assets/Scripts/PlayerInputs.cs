using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{

    PlayerController player;
    private bool fire1;
    private bool crouch;
    private bool jump;

    void Start() {
        player = gameObject.GetComponent(typeof(PlayerController)) as PlayerController;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        } else if (Input.GetButtonUp("Jump")) {
            jump = false;
        }

        if (Input.GetButtonDown("Fire1")) {
            fire1 = true;
        } else if (!Input.GetButtonUp("Fire1")) {
            fire1 = false;
        }

        //send all input to player
        player.updateInputs(Input.GetAxisRaw("Horizontal"), fire1, jump, crouch);
    }
}
