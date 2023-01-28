using TMPro;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField] private EntryPoint entryPoint;
    [SerializeField] private TMP_Text heals;
    [SerializeField] private TMP_Text ammo;

    private void Awake()
    {
        entryPoint.EnsureNotNull();
        heals.EnsureNotNull();
        ammo.EnsureNotNull();
    }
}