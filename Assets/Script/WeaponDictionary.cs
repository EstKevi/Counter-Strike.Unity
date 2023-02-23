using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Script
{
    public class WeaponDictionary : NetworkBehaviour
    {
        [SerializeField] private GameObject[] weapons = Array.Empty<GameObject>();
        private readonly Dictionary<string, GameObject> weaponKode = new();

        public int CountWeaponDictionary => weaponKode.Count;
        private void Awake()
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weaponKode.Add($"weaponKey{i}", weapons[i]);
            }
        }

         public GameObject GetWeapon(int weaponKeycode)
         {
             if (weaponKeycode >= 0 && weaponKeycode <= weaponKode.Count)
             {
                 return weaponKode[$"weaponKey{weaponKeycode}"];
             }

             return weaponKode["weaponKey0"];
         }

         [ContextMenu(nameof(CheckWeaponInDictionary))]
         private void CheckWeaponInDictionary()
         {
             for (int i = 0; i < CountWeaponDictionary; i++)
             {
                 Debug.Log($"ID: {i} | {GetWeapon(i).name}");
             }
         }
    }
}