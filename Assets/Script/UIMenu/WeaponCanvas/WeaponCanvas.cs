using UnityEngine;
using UnityEngine.Events;

namespace Script.UIMenu.WeaponCanvas
{
    public class WeaponCanvas : MonoBehaviour
    {
        public UnityEvent chooseWeaponEvent = new();
    }
}