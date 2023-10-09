using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    [SerializeField] private string VersioName = "0.1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPanel;

    [SerializeField] private InputField UsernameInput;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;

    [SerializeField] private GameObject StartButton;


    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersioName);
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    private void Start()
    {
       UsernameMenu.SetActive(true);
    }

    public void ChangeUserNameInput()
    {
        if (UsernameInput.text.Length >= 3)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
    }

    public void SetUserName()
    {
        UsernameMenu.SetActive(false);
        PhotonNetwork.playerName = UsernameInput.text;
    }

    private void CreateGame()
    {
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { MaxPlayers = 5 }, null);
    }

    private void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainGame");
    }
}

//You can use the code she is doing in pun 2, you have to add using Photon.Pun; using Photon.Realtime; you need to write .NickName instead of .Username and you have to change monobehaviour to monobehaviourpuncallbacks