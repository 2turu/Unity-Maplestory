
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class NetworkedPlayer : PlayerBehavior
{
    //A toggle event, see the merry fragmas 3.0 (https://unity3d.com/learn/tutorials/topics/multiplayer-networking/merry-fragmas-30-multiplayer-fps-foundation)
    //for a proper explanation
    [System.Serializable]
    public class ToggleEvent : UnityEvent<bool> { }

    //make an event for each RPC, so other scripts can acces them 
    public event System.Action<RpcArgs> SetupPlayerEvent;
    //also make an event for the network start as some components need that as well
    public event System.Action NetworkStartEvent;

    //the player
    PlayerController player;

    //Use the toggle event class
    //See the inspector for this
    [SerializeField]
    ToggleEvent ownerScripts;

    //the player's HUD canvas
    [SerializeField]
    //private GameObject HUD;

    //The player's camera
    //private Camera playerCamera;

    /*
    public GameObject Sani {
        get {
            return Sani;
        }
    }
    */
    /*
    public Camera PlayerCamera {
        get {
            return playerCamera;
        }
    }
    */

    private void Start() {
        player = gameObject.GetComponent(typeof(PlayerController)) as PlayerController;
    }

    /// <summary>
    /// Called when the network object is ready and initialized
    /// </summary>
    protected override void NetworkStart() {
        base.NetworkStart();

        //make a dynamic bool depending on we are the owner or not, and define the logic in the inspector.
        //eg some scripts should be disabled on non owners
        ownerScripts.Invoke(networkObject.IsOwner);

        //Get the player camera
        //playerCamera = GetComponentInChildren<Camera>();


        //Disable the camera if we aren't the owner
        if (!networkObject.IsOwner) {
            //playerCamera.gameObject.SetActive(false);
        } else if (networkObject.IsOwner) {
            //Enable the HUD if we are the owner (it's disabled in the prefab)
            //HUD.SetActive(true);
            //call the network start event
            if (NetworkStartEvent != null) {
                NetworkStartEvent();
            }
        }

        if (NetworkManager.Instance.Networker is IServer) {
            //here you can also do some server specific code
        } else {
            //setup the disconnected event
            NetworkManager.Instance.Networker.disconnected += DisconnectedFromServer;

        }

    }

    /// <summary>
    /// Called when a player disconnects
    /// </summary>
    /// <param name="sender"></param>
    private void DisconnectedFromServer(NetWorker sender) {
        NetworkManager.Instance.Networker.disconnected -= DisconnectedFromServer;

        MainThreadManager.Run(() => {
            //Loop through the network objects to see if the disconnected player is the host
            foreach (var no in sender.NetworkObjectList) {
                if (no.Owner.IsHost) {
                    BMSLogger.Instance.Log("Server disconnected");
                    //Should probably make some kind of "You disconnected" screen. ah well
                    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                }
            }

            NetworkManager.Instance.Disconnect();
        });
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (networkObject.IsOwner) {

            //set the position
            networkObject.position = transform.position;
            networkObject.fire1 = player.keyFire1;
            networkObject.horizontal = player.keyHorizontal;
            networkObject.jump = player.keyJump;
            networkObject.crouch = player.keyCrouch;

        } else //non owner, meaning a remote player
          {
            //receive all NCW fields and use them
            transform.position = networkObject.position;
            player.keyFire1 = networkObject.fire1;
            player.keyHorizontal = networkObject.horizontal;
            player.keyJump = networkObject.jump;
            player.keyCrouch = networkObject.crouch;
        }

    }

    /// <summary>
    /// The RPC for setting up the player (skin, playername), calls an event
    /// </summary>
    /// <param name="args"></param>
    public override void SetupPlayer(RpcArgs args) {
        if (SetupPlayerEvent != null) {
            SetupPlayerEvent(args);
        }
    }
}
