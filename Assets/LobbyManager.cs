using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public TMPro.TextMeshProUGUI roomName;

    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    public GameObject playButton;
    public TMPro.TextMeshProUGUI playerCount;

    private void Start(){
        PhotonNetwork.JoinLobby();
    }

    public void OnClickCreate(){
        if(roomInputField.text.Length >= 1){
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions(){MaxPlayers = 10});
        }
    }

    public override void OnJoinedRoom(){
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        playerCount.enabled = true;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){

        if(Time.time >= nextUpdateTime){
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;

        }
    }

    void UpdateRoomList(List<RoomInfo> list){
        foreach(RoomItem item in roomItemsList){
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach(RoomInfo room in list){
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName){
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom(){
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom(){
        playerCount.enabled = false;
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        
    }

    public override void OnConnectedToMaster(){
        PhotonNetwork.JoinLobby();
    }

    private void Update(){
        if(playerCount.enabled == true){
            playerCount.text = "Players In Lobby: " + PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        }
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 1){
            playButton.SetActive(true);
        }
        else{
            playButton.SetActive(false);
        }
    }

    public void OnClickPlayButton(){
        PhotonNetwork.LoadLevel("Game");
    }

}
