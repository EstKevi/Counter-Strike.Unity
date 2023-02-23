using UnityEngine;
using UnityEngine.Events;

namespace Script.UIMenu.WeaponCanvas
{
    public class WeaponCanvas : MonoBehaviour
    {
        public UnityEvent<int> chooseWeaponEvent = new();

        private void Start()
        {
            chooseWeaponEvent.AddListener(_ => gameObject.SetActive(false));
        }
    }
}