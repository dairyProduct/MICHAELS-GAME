using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class bullet : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    Vector3 mospos;
    Camera cam;
    float delay = 3f;
    private PhotonView ID;
    private int bulletID;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        ID = info.photonView;
        // ...
    }
    void Awake(){
        //gets the direction and rotation from playerpos and mousepos
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mospos = cam.ScreenToWorldPoint(Input.mousePosition);
        var direction = mospos - transform.position;
        var rotate = transform.position - mospos;
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized*15;

        float rot = Mathf.Atan2(rotate.x, -rotate.y)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

        string s = photonView.ViewID.ToString();
        char[] chars = s.ToCharArray();
        bulletID = int.Parse(chars[0].ToString());
    }
    void FixedUpdate(){
        delay -= Time.fixedDeltaTime;
        if(delay<=0f){
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.GetComponent<PhotonView>() != null){
            string s = col.gameObject.GetComponent<PhotonView>().ViewID.ToString();
            char[] chars = s.ToCharArray();
            int playerWhoShotID = int.Parse(chars[0].ToString());

            if(col.gameObject.tag == "Player" &&  playerWhoShotID != bulletID){
                RPC_Dmg(col.gameObject);
                Destroy(gameObject);
            }
            else if(col.gameObject.tag == "enimy"){
                col.gameObject.GetComponent<EnimyHealth>().health -= 1;
                col.gameObject.transform.position = col.gameObject.transform.position + new Vector3(GetComponent<Rigidbody2D>().velocity.x*.02f, GetComponent<Rigidbody2D>().velocity.y*.02f, 0f);
                col.gameObject.GetComponent<Follow>().speed += 0.75f;
                Destroy(gameObject);
            }
            else if(col.gameObject.tag != "Player" && col.gameObject.tag != "PlayerCollider" && col.gameObject.tag != "Swing"){
                Destroy(gameObject);
            }
        }
        else if(col.gameObject.GetComponent<PhotonView>() == null && col.gameObject.name != "PlayerTrigger" && col.gameObject.name != "player" && col.gameObject.tag != "Swing"){
            Destroy(gameObject);
        }
    }

    void RPC_Dmg(GameObject player){
        playerHealth life = player.GetComponent<playerHealth>();
        life.RPC_ChangeHealth(1);
        
    }
}
