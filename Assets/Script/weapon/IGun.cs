using UnityEngine;

namespace Script.weapon
{
    public interface IGun
    {
        void Shoot(Collider obj);
        void Reload();
    }
}