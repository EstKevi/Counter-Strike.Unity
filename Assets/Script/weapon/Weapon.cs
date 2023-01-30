using System.Collections;
using Script.player.Player.heal;
using Unity.Netcode;
using UnityEngine;

namespace Script.weapon.AK_47
{
    public class Weapon : NetworkBehaviour, IGun
    {
        [SerializeField] private int ammo;
        [SerializeField] private int stock;
        [SerializeField] private int damage;
        [SerializeField] private float shootSpeed;
        [SerializeField] private float reloadSpeed;
        [SerializeField] private bool canShoot;
        [SerializeField] private bool canReload;
        private float previousShot;
        private int originalMeaningAmmo;

        public int Ammo => ammo;

        public int Stock => stock;

        private void Awake() => originalMeaningAmmo = ammo;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void Shoot(Collider obj)
        {
            if (canShoot is false || ammo == 0)
            {
                Reload();
                return;
            }

            if (!(Time.time - previousShot > shootSpeed)) return;

            print("bang");
            ammo--;

            previousShot = Time.time;

            if (!obj.TryGetComponent<IDamageable>(out var heal)) return;
            
            heal.Apply(damage);
            print("damage");
        }

        public void Reload()
        {
            if (canReload && ammo < originalMeaningAmmo && stock != 0)
                StartCoroutine(ReloadWeapon());
        }

        private IEnumerator ReloadWeapon()
        {
            print("reload is started");
            ChangeWeaponState();
            
            yield return new WaitForSecondsRealtime(reloadSpeed);

            var cartridges = originalMeaningAmmo - ammo;

            while (stock - cartridges < 0)
            {
                cartridges--;
            }

            stock -= cartridges;
            ammo += cartridges;
            
            ChangeWeaponState();

            print("reload is ended");
        }

        private void ChangeWeaponState()
        {
            canReload = !canReload;
            canShoot = !canShoot;
        }
    }
}