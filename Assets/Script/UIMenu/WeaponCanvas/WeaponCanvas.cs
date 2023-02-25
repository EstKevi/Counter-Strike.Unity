using UnityEngine;
using UnityEngine.Events;

namespace Script.UIMenu.WeaponCanvas
{
    public class WeaponCanvas : MonoBehaviour
    {
        public UnityEvent<int> chooseUiWeapon = new();

        private void Start()
        {
            chooseUiWeapon.AddListener(_ => gameObject.SetActive(false));
        }
    }
}