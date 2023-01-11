using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class swingDmg : MonoBehaviourPunCallbacks
{

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.GetComponent<PhotonView>() != null && col.gameObject.tag == "Player" && col.gameObject.GetComponent<PhotonView>().ViewID != photonView.ViewID){
            col.gameObject.GetComponent<PhotonView>().RPC("RPC_ChangeHealth", RpcTarget.All, 4);
            //RPC_Dmg(col.gameObject);
        }
    }
    /*
    [PunRPC]
    void RPC_Dmg(int playerID){
        GameObject player = PhotonView.Find(playerID).gameObject;
        playerHealth life = player.GetComponent<playerHealth>();
        //life.health -= 4;
        life.RPC_ChangeHealth(4);
        Debug.Log("Good");
    }
    */
}
