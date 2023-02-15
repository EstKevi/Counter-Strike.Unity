using Script.Other;
using TMPro;
using UnityEngine;

namespace Script.UIMenu.playerCanvas
{
    public class PlayerInterface : MonoBehaviour
    {
        [SerializeField] private TMP_Text healsText;
        [SerializeField] private TMP_Text ammoText;

        private void Awake()
        {
            healsText.EnsureNotNull();
            ammoText.EnsureNotNull();
        }

        public void PlayerStatsSet(int heal, int ammo, int stock)
        {
            healsText.text = $"HP {heal}";
            ammoText.text = $"{ammo} | {stock}";
        }
    }
}