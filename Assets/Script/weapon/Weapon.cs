using System.Collections;
using Script.player.Player.heal;
using Unity.Netcode;
using UnityEngine;

namespace Script.weapon.AK_47
{
    public class Weapon : NetworkBehaviour, IGun
    {
        [SerializeField] private int ammo = 30;
        [SerializeField] private int stock = 90;
        [SerializeField] private float damage = 23;
        [SerializeField] private float shootSpeed = 0.1f;
        [SerializeField] private float reloadSpeed = 3.5f;
        [SerializeField] private bool canShoot;
        [SerializeField] private bool canReload;
        private float previousShot;

        public int Ammo => ammo;
        public int Stock => stock;

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
            { previousShot = Time.time; }

            if (obj.TryGetComponent<IDamageable>(out var heal))
            {
                heal.Apply(damage);
                print("damage");
            }
        }

        public void Reload()
        {
            if (canReload && ammo < 30 && stock != 0)
                StartCoroutine(ReloadWeapon());
        }

        private IEnumerator ReloadWeapon()
        {
            print("reload is started");
            ChangeWeaponState();
            
            yield return new WaitForSecondsRealtime(reloadSpeed);

            var cartridges = 30 - ammo;

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