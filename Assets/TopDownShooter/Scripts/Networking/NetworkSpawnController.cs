using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace TopDownShooter.Networking
{
    public class NetworkSpawnController : MonoBehaviour, INetworkRunnerCallbacks
    {
        public NetworkingPlayer networkPlayerPrefab;
        public Transform[] spawnPoints = new Transform[5];
        private static int spawnCounter = 0;

        private void Start()
        {
            
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            Debug.Log("Connected to server");
        }

        public void OnDisconnectedFromServer(NetworkRunner runner)
        {
            
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            if (NetworkingPlayer.Local != null)
            {
                input.Set(NetworkingPlayer.Local.characterControl.PlayerInput.GetInputData());
            }
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer)
            {
                runner.Spawn(networkPlayerPrefab, new Vector3(14.32f, 0.2f, 6f), Quaternion.identity, player);
                spawnCounter++;
            }

            Debug.Log($"Player {spawnCounter} joined");
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
            
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
            
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
            
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
            
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        {
            
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
            
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
            
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
            
        }
    }
}
