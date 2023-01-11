using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class push : MonoBehaviourPunCallbacks
{

    void OnCollisionEnter2D(Collision2D col){
        moveObject(col.gameObject);
    }

    void moveObject(GameObject player){
        //lets the player be the owner so he can push
        this.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
    }
/*
    public override void OnOwnershipRequest(object[] viewAndPlayer){
        PhotonView view = viewAndPlayer[0] as PhotonView;
        PhotonPlayer requestingPlayer = viewAndPlayer[1] as PhotonPlayer;

        base.photonView.TransferOwnership(requestingPlayer);
    }
    */
}
