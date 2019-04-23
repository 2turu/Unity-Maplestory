using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;

/// <summary>
/// A helper class used to instantiate the gamemode network object
/// </summary>
public class InstantiateGameMode : MonoBehaviour {
    // Use this for initialization
    void Start() {
        //Only have the gamemode on the server
        if (NetworkManager.Instance.IsServer) {
            NetworkManager.Instance.InstantiateGameMode();
        }
    }
}
