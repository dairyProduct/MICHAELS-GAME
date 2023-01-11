using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public TMP_InputField usernameInput;
    public TMPro.TextMeshProUGUI buttonText, warningText;
    public GameObject CTS, Options;
    
    public void OnClickConnect(){
        if(usernameInput.text.Length >= 1){
            //This officially sets the players online username
            //***Username+Password?***
            PhotonNetwork.NickName = usernameInput.text;
            buttonText.text = "Connecting...";
            //Connects the player to the server.
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
        else{
            warningText.enabled = true;
        }
    }

    public override void OnConnectedToMaster(){
        SceneManager.LoadScene("Lobby");
    }

    public void OnClickOptions(){
        CTS.SetActive(false);
        Options.SetActive(true);
    }

    public void OnClickOptionsBack(){
        CTS.SetActive(true);
        Options.SetActive(false);
    }

    
}
