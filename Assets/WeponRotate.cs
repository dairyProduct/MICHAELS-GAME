using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeponRotate : MonoBehaviourPunCallbacks
{
    public GameObject myPlayer;
    public Animator anim;
    private PhotonView view;
 
    private void FixedUpdate()
    {
        if(photonView.IsMine){
            RPC_Rotate();
        }
    }
    [PunRPC]
    void RPC_Rotate(){
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        int quadrent = Mathf.RoundToInt((rotationZ+45) / 90);
        if(quadrent == -1){
            quadrent = 3;
        }
        else if(quadrent == 0){
            quadrent = 4;
        }
        anim.SetInteger("Quadrent", quadrent);
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (rotationZ < -90 || rotationZ > 90)
        {
            if(myPlayer.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
                myPlayer.GetComponent<SpriteRenderer>().flipX = true;
            } 
            else if (myPlayer.transform.eulerAngles.y == 180) {
                myPlayer.GetComponent<SpriteRenderer>().flipX = true;
                transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
            }
        }
        else{
            myPlayer.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
