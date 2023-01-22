using System;
using Unity.Netcode;
using UnityEngine;

namespace Script.weapon.AK_47
{
    public class AkWeapon : NetworkBehaviour, IGun
    {
        [SerializeField] private int ammo = 30;
        [SerializeField] private int stock = 90;
        [SerializeField] private float shootSpeed = 0.01f;
        [SerializeField] private float reloadSpeed = 3.5f;
        
        public void Shoot()
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }
    }
}