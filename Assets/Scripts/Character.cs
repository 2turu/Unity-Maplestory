using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Using this as a data container

[CreateAssetMenu (menuName = "Character")]
public class Character : ScriptableObject
{
    public string characterName = "Default";
    public int startingHp = 100;

    public Skills[] characterSkills;
    //DOES NOT RECEIVE THESE CALLBACKS
    /*
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    */
}
