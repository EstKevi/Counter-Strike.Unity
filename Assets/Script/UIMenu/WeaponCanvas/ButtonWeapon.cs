using Script.Other;
using Script.weapon;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script.UIMenu.WeaponCanvas
{
    public class ButtonWeapon : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;

        private void Awake()
        {
            button.EnsureNotNull().onClick.AddListener(() => Debug.Log("hello"));
            text.EnsureNotNull();
        }

        public void SetValue(GameObject weapon)
        {
            var gun = weapon.GetComponent<IGun>();
            text.text = $"{weapon.name} | ammo: {gun.Ammo} | stock: {gun.Stock}";
        }
    }
}