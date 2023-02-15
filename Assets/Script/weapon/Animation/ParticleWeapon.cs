using Script.Other;
using UnityEngine;

namespace Script.weapon
{
    [RequireComponent(typeof(Weapon))]
    public class ParticleWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private ParticleSystem particleShoot;

        private void Awake()
        {
            weapon.EnsureNotNull();
            particleShoot.EnsureNotNull();
        }

        private void Start()
        {
            particleShoot.Stop();
            weapon.particleShootEvent.AddListener((() => particleShoot.Play()));
        }
    }
}