using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawn player at vector location
public class SpawnPlayer : MonoBehaviour {
    // Use this for initialization
    void Start() {
        //Instantiate the player
        NetworkManager.Instance.InstantiatePlayer(position: new Vector3(0,5,0));
    }
}
