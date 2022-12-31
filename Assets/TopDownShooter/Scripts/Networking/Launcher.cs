using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace TopDownShooter
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public PhotonView playerPrefab;

        public Vector3 spawnPosition;

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master");
            PhotonNetwork.JoinRandomOrCreateRoom();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined room");
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
        }
    }
}
