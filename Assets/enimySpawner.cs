using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enimySpawner : MonoBehaviour
{
    float timer = 1f;
    public GameObject player, zom;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timer <= 0f){
            timer = 1.5f;
            GameObject b = Instantiate(zom, player.transform.position + new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 0f), Quaternion.identity) as GameObject;
        }
        timer -= Time.fixedDeltaTime;
        
    }
}
