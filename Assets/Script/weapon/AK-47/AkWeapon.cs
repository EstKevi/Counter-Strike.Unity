using System;
using System.Collections;
using UnityEngine;

namespace Script.weapon.AK_47
{
    public class AkWeapon : MonoBehaviour, IGun
    {
        [SerializeField] private int ammo = 30;
        [SerializeField] private int stock = 90;
        [SerializeField] private float shootSpeed = 0.1f;
        [SerializeField] private float reloadSpeed = 3.5f;
        [SerializeField] private bool canShoot;
        [SerializeField] private bool canReload;
        private float previousShot;

        private void Start()
        {
            canShoot = true;
            canReload = true;
        }

        public void Shoot()
        {
            if (canShoot is false) return;
            if (ammo != 0)
            {
                if (Time.time - previousShot > shootSpeed)
                {
                    print("bang");
                    ammo--;
                    previousShot = Time.time;
                }
                return;
            }

            Reload();
        }

        public void Reload()
        {
            if (canReload && ammo < 30 && stock != 0)
                StartCoroutine(ReloadWeapon());
        }

        private IEnumerator ReloadWeapon()
        {
            print("reload is started");

            canShoot = false;
            canReload = false;

            yield return new WaitForSecondsRealtime(reloadSpeed);

            canShoot = true;
            canReload = true;

            if (ammo == 0 && stock >= 30)
            {
                stock -= 30;
                ammo += 30;

                print("reload is ended");
                yield break;
            }

            var cartridges = 30 - ammo;

            while (stock - cartridges < 0) 
                cartridges--;

            stock -= cartridges;
            ammo += cartridges;

            print("reload is ended");
        }
    }
}