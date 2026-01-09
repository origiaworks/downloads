using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RandomMuchMaker : MonoBehaviourPunCallbacks {

    [SerializeField, Range(0, 20)]
    public byte maxRoomPlayerCount;

    public GameObject photonObject;

    //接続を開始する
    public void StartPhoton() {
        Debug.Log("Photonへ接続開始");
        PhotonNetwork.ConnectUsingSettings();
    }

    //マスタに接続した時
    public override void OnConnectedToMaster() {
        Debug.Log("マスタへ接続成功");
        PhotonNetwork.JoinRandomRoom();
    }

    //ロビーに接続した時
    public override void OnJoinedLobby() {
        Debug.Log("ロビーへ接続成功");
        base.OnJoinedRoom();
    }

    //ルームへの接続に失敗した時
    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.LogFormat("コード:{0}\n詳細:{1}",returnCode,message);        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxRoomPlayerCount;
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    //ルームに接続した時
    public override void OnJoinedRoom() {
        Debug.Log("ルームへ接続成功");
        PhotonNetwork.Instantiate(
            photonObject.name,
            new Vector3(0f, 1f, 0f),
            Quaternion.identity
        );

        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera.GetComponent<UnityChan.ThirdPersonCamera>().enabled= true;
    }

}
