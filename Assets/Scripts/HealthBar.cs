using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    GameObject enemyObject;
    Enemy enemyScript;

    public RectTransform bar;
    // Start is called before the first frame update
    void Start()
    {
        enemyObject = GameObject.Find("Papulatus");
        enemyScript = enemyObject.GetComponent<Enemy>();
    }

    void Update()
    {
        SetSize(enemyScript.currentHealth / enemyScript.maxHealth);
    }

    public void SetSize(float percentHealth)
    {
        if(percentHealth >= 0)
        {
            bar.localScale = new Vector3(percentHealth, 1f, 1f);
        }
        else
        {
            bar.localScale = new Vector3(0f, 1f, 1f);
        }
    }
}
