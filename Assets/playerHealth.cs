using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class playerHealth : MonoBehaviourPunCallbacks
{
    public int health;
    public int maxHealth;
    public Slider healthBar;
    public GameObject slider, player;
    public float delay = 1f;
    public ParticleSystem hit;
    // Start is called before the first frame update

    void Start()
    {
        healthBar = slider.GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    void Update(){
        if(photonView.IsMine){
            if(Input.GetKeyDown(KeyCode.R) && healthBar.value <= 0){
                photonView.RPC("RPC_Respawn", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void RPC_Respawn(){
        player.GetComponent<Movement>().enabled = true;
        player.transform.GetComponentInChildren<pistal>().enabled = true;
        player.transform.GetComponentInChildren<WeponRotate>().enabled = true;
        player.transform.GetComponentInChildren<WeponSelect>().enabled = true;
        transform.position = transform.position + new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0f);
        healthBar.value = maxHealth;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.transform.GetComponentInChildren<SpriteRenderer>().enabled = true;

    }

    // Update is called once per frame
    [PunRPC]
    public void RPC_ChangeHealth(int amount){
        healthBar.value -= amount;
        Debug.Log(healthBar.value);
        hit.Play();
        StartCoroutine(DamagedEffect());            
        if(healthBar.value <= 0){
            player.GetComponent<Movement>().enabled = false;
            player.transform.GetComponentInChildren<pistal>().enabled = false;
            player.transform.GetComponentInChildren<WeponRotate>().enabled = false;
            player.transform.GetComponentInChildren<WeponSelect>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<SpriteRenderer>().enabled = false;
            player.transform.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }


    IEnumerator DamagedEffect(){
        player.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.3f);
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
