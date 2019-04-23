using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour {
    //to grab health info, may need to change
    GameObject playerObject;
    PlayerController playerScript;

    //get game object RectTransform
    public RectTransform bar;

    // Start is called before the first frame update
    void Start() {
        playerObject = GameObject.Find("Sani");
        playerScript = playerObject.GetComponent<PlayerController>();
    }

    void Update() {
        SetSize(playerScript.currentHealth / playerScript.maxHealth);
    }

    public void SetSize(float percentHealth) {
        if(percentHealth >= 0) {
            bar.localScale = new Vector3(percentHealth, 1f, 1f);
        } else {
            bar.localScale = new Vector3(0f, 1f, 1f);
        }
    }
}
