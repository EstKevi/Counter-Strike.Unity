using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace Script.player.RespawnPlayer
{
    [RequireComponent(typeof(Player))]
    public class Respawn : NetworkBehaviour
    {
        [SerializeField] private float timeForRespawn;
        private NetworkVariable<Vector3> respawnPosition = new(
            Vector3.zero,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        public UnityEvent playerRespawned = new();
        
        public void RespawnPlayer(Transform point)
        {
            if (IsOwner)
            {
                respawnPosition.Value = point.position;
                StartCoroutine(RespawnPlayerWait(timeForRespawn));
            }
        }

        private IEnumerator RespawnPlayerWait(float time)
        {
            if (!IsOwner) yield return null;
            yield return new WaitForSecondsRealtime(time);
            RespawnPlayerServerRpc();
        }

        [ServerRpc]
        private void RespawnPlayerServerRpc()
        {
            transform.position = respawnPosition.Value;
            RespawnPlayerClientRpc();
        }
        
        [ClientRpc]
        private void RespawnPlayerClientRpc()
        {
            transform.position = respawnPosition.Value;
            playerRespawned.Invoke();
        }
    }
}