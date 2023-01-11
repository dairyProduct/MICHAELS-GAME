using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] playerPrefabs;

    private void Start()
    {
        int randx = Random.Range(-10, 10);
        int randy = Random.Range(-10, 10);
        GameObject playerToSpawn = playerPrefabs[0];
        GameObject myPlayer = PhotonNetwork.Instantiate(playerToSpawn.name, new Vector3(randx, randy, 0f), Quaternion.identity);
        myPlayer.GetComponentInChildren<Camera>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
