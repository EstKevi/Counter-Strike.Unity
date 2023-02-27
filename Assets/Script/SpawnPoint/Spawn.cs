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
        [SerializeField] private NetworkManager networkManager;

        private void Awake()
        {
            networkManager.EnsureNotNull();
            
            foreach (var points in FindObjectsOfType<SpawnPoint>())
                spawnPoints.Add(points);

            if (spawnPoints.Count == 0)
            {
                Debug.LogWarning("SpawnPoints not found");
            }
        }

        public Transform GiveSpawnPoint()
        {
            foreach (var point in spawnPoints)
            {
                if (point.PlayersInPoint is false)
                {
                    return point.transform;
                }
            }
            
            var index = Random.Range(0, spawnPoints.Count);
            return spawnPoints[index].transform;
        }
    }
}