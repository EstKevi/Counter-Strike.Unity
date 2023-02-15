using DG.Tweening;
using Script.Other;
using UnityEngine;

namespace Script.weapon.Animation
{
    [RequireComponent(typeof(Weapon))]
    public class AnimationReloadWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private Transform animatedPoint;
        [SerializeField] private Vector3 endValue;
        
        private void Awake()
        {
            weapon.EnsureNotNull();
            animatedPoint.EnsureNotNull();
        }

        private void Start() => weapon.reloadWeaponEvent.AddListener(AnimationWeapon);

        private void AnimationWeapon(float time)
        {
            //TODO normal time for animation
            var firstPartAnimated = time / 2;
            animatedPoint.transform.DOLocalRotate(endValue, firstPartAnimated).SetLoops(2,LoopType.Yoyo);
        }
    }
}