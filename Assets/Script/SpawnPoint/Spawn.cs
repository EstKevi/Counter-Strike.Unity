using System.Collections.Generic;
using Script.Other;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.SpawnPoint
{
    [RequireComponent(typeof(NetworkObject))]
    public class Spawn : NetworkBehaviour
    {
        [SerializeField] private List<SpawnPoint> spawnPoints = new();
        [SerializeField] private EntryPoint entryPoint;

        private void Awake()
        {
            entryPoint.EnsureNotNull();
            
            foreach (var points in FindObjectsOfType<SpawnPoint>())
                spawnPoints.Add(points);
            
            if (spawnPoints.Count == 0)
                Debug.LogWarning("SpawnPoints not found");
            
            entryPoint.EntryManager.OnClientConnectedCallback += SpawnPlayer;
        }

        public void SpawnPlayer(ulong id)
        {
            if (spawnPoints.Count == 0) return;
            
            var player = entryPoint.EntryManager.ConnectedClients[id];
            foreach (var point in spawnPoints)
            {
                if (!point.PlayersInPoint)
                {
                    player.PlayerObject.transform.position = point.transform.position;
                    return;
                }
            }

            var index = Random.Range(0, spawnPoints.Count);
            player.PlayerObject.transform.position = spawnPoints[index].gameObject.transform.position;
        }
    }
}