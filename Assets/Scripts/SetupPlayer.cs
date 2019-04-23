using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// The component responsible for the player name and player skin and the sync of both
/// </summary>
public class SetupPlayer : MonoBehaviour {
    [Header("Script references")]
    [SerializeField]
    //A refrence to the network object script
    private NetworkedPlayer np;

    [Header("SetupPlayer")]
    //The ragdoll, used for setting the player specific skin on that as well
    //[SerializeField]
    //private GameObject ragdoll;
    //An array of all the skinned meshes on the player model
    //[SerializeField]
    //private SkinnedMeshRenderer[] SkinnedMeshes;

    //An array of possible player skin materials
    //[SerializeField]
    //private Material[] playerSkins;

    //I wonder what this is
    private string playerName;

    //properties
    public string PlayerName { get { return playerName; } }
    //public GameObject Ragdoll { get { return ragdoll; } }


    /// <summary>
    /// Called before start, setup the events from the networkplayer here, as NetworkStart is called before regular Start.
    /// Hook up all the RPC event the setup player system needs
    /// </summary>
    void Awake() {
        np.NetworkStartEvent += NetworkStart;
        np.SetupPlayerEvent += RPCSetupPlayer;
    }


    private void NetworkStart() {
        //if we are the owner, find what values we the player customization should use. (Gets saved into player prefs on the main menu)
        if (np.networkObject.IsOwner) {
            //var playerName = PlayerPrefs.GetString("PlayerName");
            //var skinIndex = PlayerPrefs.GetInt("PlayerSkinIndex");
            //call an RPC
            //np.networkObject.SendRpc(PlayerBehavior.RPC_SETUP_PLAYER, BeardedManStudios.Forge.Networking.Receivers.AllBuffered, playerName);
        }
    }

    private void RPCSetupPlayer(BeardedManStudios.Forge.Networking.RpcArgs obj) {
        //receive the rpc arguments
        //int skinIndex = obj.GetNext<int>();
        //string receivedPlayerName = obj.GetNext<string>();
        //set the player name
        //playerName = receivedPlayerName;

        //loop through the player model's skinned meshes and set the material
        //foreach (var mesh in SkinnedMeshes) {
        //    mesh.material = playerSkins[skinIndex];
        //}

        //set the ragdoll skin to the right material aswell
        //var skinnedMeshInRagdoll = ragdoll.GetComponentsInChildren<SkinnedMeshRenderer>();
        //foreach (var mesh in skinnedMeshInRagdoll) {
        //    mesh.material = playerSkins[skinIndex];
        //}
    }

    //unsubscribe to all events
    private void OnDestroy() {
        np.NetworkStartEvent -= NetworkStart;
        np.SetupPlayerEvent -= RPCSetupPlayer;
    }

}
