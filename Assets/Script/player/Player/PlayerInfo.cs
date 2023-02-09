using System;
using Script;
using Script.Other;
using Script.player.Player.Hand;
using Script.player.Player.heal;
using UniRx;
using Unity.Netcode;
using UnityEngine;

public class PlayerInfo : NetworkBehaviour
{
    [SerializeField] private EntryPoint entryPoint;
    [SerializeField] private Heals heal;
    [SerializeField] private HandWeapon hand;
    private ReactiveProperty<int> rectAmmo = new();
    private ReactiveProperty<int> rectStock = new();
    private ReactiveProperty<int> rectHeal = new();

    private void Awake()
    {
        entryPoint = FindObjectOfType<EntryPoint>().EnsureNotNull();
        heal.EnsureNotNull();
        hand.EnsureNotNull();
    }

    private void Start()
    {
        rectAmmo.Subscribe((_ => ChangeStats()));
        rectHeal.Subscribe((_ => ChangeStats()));
        rectStock.Subscribe((_ => ChangeStats()));
    }

    private void ChangeStats() => entryPoint.ChangeStats(hand.WeaponAmmo, hand.WeaponStock, heal.Heal);

    private void Update()
    {
        if (IsOwner)
        {
            rectAmmo.Value = hand.WeaponAmmo;
            rectHeal.Value = heal.Heal;
            rectStock.Value = hand.WeaponStock;
        }
    }
}