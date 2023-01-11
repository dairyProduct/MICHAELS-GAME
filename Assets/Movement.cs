using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Movement : MonoBehaviourPunCallbacks,  IPunObservable
{
    bool sprint = false;
    int gain = 0;
    Rigidbody2D rb;
    float RollDelay, staminaDelay = 0f;
    GameObject trail;
    private Inventory inventory;
    public Animator anim;
    public ParticleSystem dashSystem, sprintSystem;
    public Slider slider;
    public TextMeshProUGUI playerName;
   
    //[SerializeField] private UI_Inventory uiInventory;
    
    // Start is called before the first frame update
    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(slider.value);
        }
        else
        {
            // Network player, receive data
            slider.value = (float)stream.ReceiveNext();
        }
    }

    #endregion</code></pre>
    void Start()
    {
        inventory = new Inventory();
        //uiInventory.SetInventory(inventory);
        rb = GetComponent<Rigidbody2D>();
        trail = GameObject.Find("PlayerTrail");
        var View = GetComponent<PhotonView>();
        playerName.text = View.Owner.NickName;
    }
    // Update is called once per frame
    void Update()
    {   
        if (photonView.IsMine){
            RPC_staminaSystem();
        }
    }

    void RPC_staminaSystem(){
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        RollDelay -= Time.deltaTime;
        //sprint off: regain stamina/can shoot
        if(RollDelay < 0f && sprint == false){
            anim.SetFloat("Horizontal", hori);
            anim.SetFloat("Vertical", vert);

            rb.velocity = new Vector2(hori, vert).normalized*3;
            staminaDelay -= Time.deltaTime;
            if(Input.GetMouseButtonDown(0)){
                staminaDelay = 0.5f;
                gain = 0;
            }
            if(slider.value < slider.maxValue && staminaDelay <= 0f){
                gain = 1;
            }
        }
        //ROLL
        if(Input.GetKey("left shift") == true && RollDelay <= -0.7f && slider.value >= 1f && (hori != 0f || vert != 0f)){
            slider.value -= 1f;
            StartCoroutine(Roll());  
        
        }
        //sprint on: lose stamina
        else if(RollDelay < 0f && sprint == true ){
            anim.SetFloat("Horizontal", hori);
            anim.SetFloat("Vertical", vert);
            if(slider.value > slider.minValue){
                gain = 2;
            }
            rb.velocity = new Vector2(hori, vert).normalized*6;
            ParticleSystem.VelocityOverLifetimeModule runEffect = sprintSystem.velocityOverLifetime;
            runEffect.x = -rb.velocity.x;
            runEffect.y = -rb.velocity.y;
            if(rb.velocity == Vector2.zero || slider.value <= 0f){
                sprint = false;
                sprintSystem.Stop();
            }
            
        }
        //start sprinting
        if(Input.GetKeyDown(KeyCode.LeftControl) == true && sprint == false && slider.value >= 0.5f){
            sprint = true;
            sprintSystem.Play(); 
        }
        //stop sprinting
        else if(Input.GetKeyDown(KeyCode.LeftControl) == true && sprint == true){
            sprint = false;
            sprintSystem.Stop();
        }
        //if the player runs out of stamina punich them with a stamina delay
        if(slider.value <= 0f && staminaDelay <= 0f){
            staminaDelay = 3f;
        }
    }

    void FixedUpdate(){
        //this needs to be consistent amung all users since this is over time
        if(gain == 1){
            slider.value += 0.002f + slider.value * 0.003f;
        }
        else if(gain == 2){
            slider.value -= 0.01f;
        }
    }

    IEnumerator Roll(){
        if(photonView.IsMine){
            GetComponent<Collider2D>().enabled = false;   
            trail.GetComponent<TrailRenderer>().enabled = true;
            ParticleSystem.VelocityOverLifetimeModule dash = dashSystem.velocityOverLifetime;
            dash.x = -rb.velocity.x;
            dash.y = -rb.velocity.y;
            dashSystem.Play();  
            float hori = Input.GetAxisRaw("Horizontal");
            float vert = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(hori, vert).normalized*15;
            RollDelay = 0.18f;
            yield return new WaitForSeconds(0.4f);
            trail.GetComponent<TrailRenderer>().enabled = false;  

            GetComponent<Collider2D>().enabled = true; 
            yield return new WaitForSeconds(0.3f);
        }
    }

    /*
    void OnTriggerEnter2D(Collider2D col){
        ItemWorld itemWorld = col.GetComponent<ItemWorld>();
        if(itemWorld != null){
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }

    }
    */
}

