using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace Script.SpawnPoint
{
    [RequireComponent(typeof(NetworkObject))]
    [RequireComponent(typeof(Collider))]
    public class SpawnPoint : NetworkBehaviour
    {
        [SerializeField] private NetworkVariable<bool> playersInPoint = new();
        public bool PlayersInPoint => playersInPoint.Value;
        private void OnTriggerStay(Collider other)
        {
            playersInPoint.Value = true;
        }

        private void OnTriggerExit(Collider other)
        {
            playersInPoint.Value = false;
        }
    }
}