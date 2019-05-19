using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour {
    public GameObject enemyObject;
    EnemyController enemyScript;
    public RectTransform bar;

    //store health values
    float currentHealth;
    float maxHealth;

    void Start() {
        enemyScript = enemyObject.GetComponent<EnemyController>();
        maxHealth = enemyScript.getMaxHealth(); //get max health once
    }

    void Update() {
        currentHealth = enemyScript.getCurrentHealth(); //make sure current health is updated
        SetSize(currentHealth / maxHealth);
    }

    public void SetSize(float percentHealth) {
        if(percentHealth >= 0) {
            bar.localScale = new Vector3(percentHealth, 1f, 1f);
        } else {
            bar.localScale = new Vector3(0f, 1f, 1f);
        }
    }
}
