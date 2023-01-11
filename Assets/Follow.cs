using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    GameObject player;
    float strikeIn = 1f;
    public float speed = 3.75f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Vector2.Distance(transform.position, player.transform.position)<=1f){
        //    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        //}
        //else{
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized*speed;
        //}
    }
    void OnCollisionStay2D(Collision2D col){
        strikeIn -= Time.deltaTime;
        if(col.gameObject.tag == "Player" && strikeIn <= 0f){
            float delay = col.gameObject.GetComponent<playerHealth>().delay;
            strikeIn = .1f;
            if(delay <=0f){
                Debug.Log("Hit");

                col.gameObject.GetComponent<playerHealth>().health -= 1;
                col.gameObject.GetComponent<playerHealth>().delay += 2f;

            }
        }
    }
}
