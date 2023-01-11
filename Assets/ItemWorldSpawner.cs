using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;
    private void Start(){
        ItemWorld.SpawnItemWorld(gameObject.transform.position, item);
        Destroy(gameObject);
    }
}
