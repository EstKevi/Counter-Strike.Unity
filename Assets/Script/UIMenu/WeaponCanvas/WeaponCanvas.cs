using UnityEngine;
using UnityEngine.Events;

namespace Script.UIMenu.WeaponCanvas
{
    public class WeaponCanvas : MonoBehaviour
    {
        public UnityEvent<int> chooseWeaponEvent = new();

        private void Start()
        {
            chooseWeaponEvent.AddListener(arg0 => Debug.Log($"ID: {arg0}"));
        }
    }
}