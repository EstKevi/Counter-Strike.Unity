using Script.Other;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UIMenu.WeaponCanvas
{
    public class SpawnButton : MonoBehaviour
    {
        [SerializeField] private WeaponCanvas weaponCanvas;
        [SerializeField] private WeaponDictionary weaponDictionary;
        [SerializeField] private ButtonWeapon buttonWeaponPrefab;
        
        private void Start()
        {
            weaponDictionary = FindObjectOfType<WeaponDictionary>().EnsureNotNull();
            for (int i = 0; i < weaponDictionary.CountWeaponDictionary; i++)
            {
                var button = Instantiate(buttonWeaponPrefab, transform);
                // button.SetValue(weaponDictionary.GetWeapon(i));
                // button.GetComponent<Button>().onClick.AddListener(() => weaponCanvas.chooseWeaponEvent.Invoke(i));
            }
        }
    }
}