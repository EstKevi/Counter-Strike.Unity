using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Script.weapon
{
    public class WeaponDictionary : NetworkBehaviour
    {
        [SerializeField] private GameObject[] weapons = Array.Empty<GameObject>();
        private readonly Dictionary<string, GameObject> weaponKode = new();
        private const string weaponKey = "weaponKey";

        public int CountWeaponDictionary => weaponKode.Count;
        private void Awake()
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weaponKode.Add($"{weaponKey}:{i}", weapons[i]);
                if (!weaponKode[$"{weaponKey}:{i}"].TryGetComponent<IGun>(out _))
                {
                    weaponKode.Clear();
                    throw new Exception("in Dictionary must be only weapon");
                }
            }
        }

         public GameObject GetWeapon(int weaponKeycode)
         {
             if (weaponKeycode >= 0 && weaponKeycode <= weaponKode.Count)
             {
                 return weaponKode[$"{weaponKey}:{weaponKeycode}"];
             }

             return weaponKode[$"{weaponKey}0"];
         }
    }
}