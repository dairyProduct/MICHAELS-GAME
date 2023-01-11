using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Item
{
    public enum ItemType{
        Wepon,
        Tool,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite(){
        switch(itemType){
            default:
            case ItemType.Wepon: return ItemAssets.Instance.weponSprite;
            case ItemType.Tool: return ItemAssets.Instance.toolSprite;
        }
    }
    public bool IsStackable(){
        switch(itemType){
            default:
            case ItemType.Tool:
            return true;
            case ItemType.Wepon:
            return false;
        }
    }
}
