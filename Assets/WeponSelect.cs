using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class WeponSelect : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public Image imageHolder1, imageHolder2;
    public int currentWeapon = 1;
    public Animator attackAnim;
    public GameObject attack, gun, syth;

    void Awake(){
        syth.GetComponent<SpriteRenderer>().enabled = true;
        gun.GetComponent<SpriteRenderer>().enabled = false;
        attack.SetActive(false);
    }
    void Update(){
        if(photonView.IsMine){
            if((Input.GetKeyDown(KeyCode.Alpha1) || Input.mouseScrollDelta.y >= .1f || Input.mouseScrollDelta.y <= -.1f )&& currentWeapon != 1){
                imageHolder2.GetComponent<Image>().enabled = false;
                imageHolder1.GetComponent<Image>().enabled = true;
                currentWeapon = 1;
                gun.GetComponent<SpriteRenderer>().enabled = false;
                syth.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if((Input.GetKeyDown(KeyCode.Alpha2) || Input.mouseScrollDelta.y >= .1f || Input.mouseScrollDelta.y <= -.1f )&& currentWeapon != 2){
                imageHolder2.GetComponent<Image>().enabled = true;
                imageHolder1.GetComponent<Image>().enabled = false;
                currentWeapon = 2;
                gun.GetComponent<SpriteRenderer>().enabled = true;
                syth.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (Input.GetMouseButtonDown(0) && currentWeapon == 1 ){
                StartCoroutine(SwingTime());
            }
            //photonView.RPC("WeaponSwap", RpcTarget.AllBuffered);
        }
    }
    
    IEnumerator SwingTime(){
        RPC_SwingAttackOn();
        yield return new WaitForSeconds(0.3f);
        RPC_SwingAttackOff();
    }


    [PunRPC]
    void RPC_SwingAttackOn(){
        attack.SetActive(true);
        GetComponentInChildren<WeponRotate>().enabled = false;
        syth.GetComponent<SpriteRenderer>().enabled = false;
    }
    [PunRPC]
    void RPC_SwingAttackOff(){
        GetComponentInChildren<WeponRotate>().enabled = true;
        syth.GetComponent<SpriteRenderer>().enabled = true;
        attack.SetActive(false);
        if(currentWeapon == 2){
            syth.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
