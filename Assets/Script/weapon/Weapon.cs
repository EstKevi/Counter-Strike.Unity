using System.Collections;
using Script.player.heal;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace Script.weapon
{
    public class Weapon : NetworkBehaviour, IGun
    {
        [SerializeField] private NetworkVariable<int> ammo = new();
        [SerializeField] private NetworkVariable<int> stock = new();
        [SerializeField] private int damage;
        [SerializeField] private float shootSpeed, reloadSpeed;
        [SerializeField] private bool canShoot, canReload;
        public UnityEvent particleShootEvent = new();
        public UnityEvent<float> reloadWeaponEvent = new();
        
        private float previousShot;
        private int originalMeaningAmmo;

        public int Ammo => ammo.Value;

        public int Stock
        {
            get => stock.Value;
            set => stock.Value = value;
        }

        private void Awake() => originalMeaningAmmo = ammo.Value;

        // ReSharper disable Unity.PerformanceAnalysis
        public void Shoot(Collider obj)
        {
            if (canShoot is false || ammo.Value == 0)
            {
                Reload();
                return;
            }

            if (!(Time.time - previousShot > shootSpeed)) return;
            print("bang");
            ammo.Value--;
            previousShot = Time.time;
            particleShootEvent.Invoke();

            if (!obj.TryGetComponent<IDamageable>(out var heal)) return;
            heal.ApplyDamage(damage);
        }

        public void Reload()
        {
            if (canReload && ammo.Value < originalMeaningAmmo && stock.Value != 0)
            {
                StartCoroutine(ReloadWeapon());
            }
        }

        private IEnumerator ReloadWeapon()
        {
            ChangeWeaponState();
            reloadWeaponEvent.Invoke(reloadSpeed);
            
            yield return new WaitForSecondsRealtime(reloadSpeed);

            var cartridges = originalMeaningAmmo - ammo.Value;

            while (stock.Value - cartridges < 0)
            {
                cartridges--;
            }

            stock.Value -= cartridges;
            ammo.Value += cartridges;
            
            ChangeWeaponState();
        }

        private void ChangeWeaponState()
        {
            canReload = !canReload;
            canShoot = !canShoot;
        }
    }
}