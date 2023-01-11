using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class pistal : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject bullet;
    private float shootDelay = 0f;
    public GameObject player;
    private Vector2 difference;
    public ParticleSystem system;
    public Animator anim;
    private float shake = 0.25f;
    private PhotonView view;
    public bool IsFiring;

    // Update is called once per frame
    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(IsFiring);
        }
        else
        {
            // Network player, receive data
            IsFiring = (bool)stream.ReceiveNext();
        }
    }

    #endregion</code></pre>
    void Update()
    {
        if(photonView.IsMine){
            int gunEquiped = player.GetComponent<WeponSelect>().currentWeapon;
            if(Input.GetMouseButtonDown(0) && shootDelay <= 0f && gunEquiped == 2){
                RPC_Shoot();
            }
            if(shake <= -0.3f ){
                IsFiring = false;
                anim.SetBool("shot", IsFiring);
            }
            shake -= Time.deltaTime;
        }


    }


    void FixedUpdate(){
        shootDelay -= Time.fixedDeltaTime;
    }


    [PunRPC]
    void RPC_Shoot(){
        //create bullet and direct it
        object[] info = new object[player.GetComponent<PhotonView>().ViewID];
        IsFiring = true;
        anim.SetBool("shot", IsFiring);
        PhotonNetwork.Instantiate(bullet.name, transform.position, Quaternion.identity, 0, info);
        shootDelay = 0.75f;
        system.Play();
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        difference.Normalize();
        player.GetComponent<Rigidbody2D>().AddForce(-difference*500, ForceMode2D.Force);
        shake = 0f;
                
    }

}
