using Script;
using Script.player.Player.Hand;
using Unity.Netcode;
using UnityEngine;

public class PlayerInfo : NetworkBehaviour
{
    [SerializeField] private EntryPoint entryPoint;
    [SerializeField] private Heals heal;
    [SerializeField] private HandWeapon hand;

    private void Awake()
    {
        entryPoint = FindObjectOfType<EntryPoint>().EnsureNotNull();
        heal.EnsureNotNull();
        hand.EnsureNotNull();
    }

    private void Update()
    {
        if (IsOwner)
        {
            entryPoint.ChangeStats(hand.WeaponAmmo, hand.WeaponStock, heal.Heal);
            //TODO сделать при обновлении значений
        }
    }
}