using Unity.Netcode;
using UnityEngine;

namespace Script.SpawnPoint
{
    public class SpawnPoint : NetworkBehaviour
    {
        [SerializeField] private NetworkVariable<bool> playersInPoint = new();

        private void Awake() => GetComponent<MeshRenderer>().enabled = false;

        public bool PlayersInPoint => playersInPoint.Value;

        private void OnTriggerStay(Collider _)
        {
            if (IsServer)
            {
                playersInPoint.Value = true;
            }
        }

        private void OnTriggerExit(Collider _)
        {
            if(IsServer)
            {
                playersInPoint.Value = false;
            }
        }
    }
}