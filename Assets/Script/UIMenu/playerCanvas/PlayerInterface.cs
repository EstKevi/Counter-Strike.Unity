using System;
using Script.Other;
using TMPro;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField] private TMP_Text healsText;
    [SerializeField] private TMP_Text ammoText;

    private void Awake()
    {
        healsText.EnsureNotNull();
        ammoText.EnsureNotNull();
    }

    public void PlayerStatsSet(int ammo, int stock, int heal)
    {
        healsText.text = $"HP {heal}";
        ammoText.text = $"{ammo} | {stock}";
    }
}