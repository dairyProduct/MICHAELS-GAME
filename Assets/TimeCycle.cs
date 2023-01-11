using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCycle : MonoBehaviour
{
    Image lightLvl;
    bool changing = true;
    float dayTime = 10f;
    float nightTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        lightLvl = GetComponent<Image>();
        StartCoroutine (CoUpdate());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine (CoUpdate());
    }
    public IEnumerator CoUpdate(){
        var day = new Color(0.01f,0f,0.1f,0f);
        var night = new Color(0.01f,0f,0.1f,0.57f);
        if(changing == true && dayTime <= 0f){
            lightLvl.color = Color.Lerp(lightLvl.color, day, 0.0005f);
            if(lightLvl.color == day){
                changing = false;
            }
        }
        else if(changing == false && nightTime <= 0f){
            lightLvl.color = Color.Lerp(lightLvl.color, night, 0.0005f);
            if(lightLvl.color == night){
                changing = true;
            }
        }
        if(changing == true){
            nightTime = 350f;
            dayTime-=Time.deltaTime;
        }
        else if(changing == false){
            dayTime = 500f;
            nightTime -= Time.deltaTime;
        }
        yield return null;
    }
}
