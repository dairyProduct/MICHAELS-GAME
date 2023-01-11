using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnimyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    Slider healthBar;
    public GameObject slider;
    public ParticleSystem system;
    // Start is called before the first frame update
    void Awake()
    {
        healthBar = slider.GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(health == maxHealth){
            slider.SetActive(false);
        }
        else{
            slider.SetActive(true);
            healthBar.value = health;
            system.Play();
        }
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
