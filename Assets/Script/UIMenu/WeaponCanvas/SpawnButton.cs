using Script.Other;
using UnityEngine;

namespace Script.UIMenu.WeaponCanvas
{
    public class SpawnButton : MonoBehaviour
    {
        [SerializeField] private WeaponDictionary weaponDictionary;
        [SerializeField] private ButtonWeapon buttonWeaponPrefab;
        
        private void Start()
        {
            weaponDictionary = FindObjectOfType<WeaponDictionary>().EnsureNotNull();
            for (int i = 0; i < weaponDictionary.CountWeaponDictionary; i++)
            {
                Instantiate(buttonWeaponPrefab, transform).SetValue(weaponDictionary.GetWeapon(i));
            }
        }
    }
}