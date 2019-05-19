
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class NetworkedEnemy : EnemyBehavior
{
    //A toggle event, see the merry fragmas 3.0 (https://unity3d.com/learn/tutorials/topics/multiplayer-networking/merry-fragmas-30-multiplayer-fps-foundation)
    //for a proper explanation
    public class ToggleEvent : UnityEvent<bool> { }

    //make an event for each RPC, so other scripts can acces them 
    //public event System.Action<RpcArgs> SetupPlayerEvent;
    //also make an event for the network start as some components need that as well
    public event System.Action NetworkStartEvent;

    EnemyController enemy;

    private void Start() {
        enemy = gameObject.GetComponent(typeof(EnemyController)) as EnemyController;
    }

    /// <summary>
    /// Called when the network object is ready and initialized
    /// </summary>
    protected override void NetworkStart() {
        base.NetworkStart();

        //call the network start event
        if (NetworkStartEvent != null) {
            NetworkStartEvent();
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
        transform.position = networkObject.position;
    }

}
