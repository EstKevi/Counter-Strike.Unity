using DG.Tweening;
using Script.player.Player.Hand;
using UnityEngine;

namespace Script.Bonus
{
    public class AmmoBonus : MonoBehaviour, IBonus
    {
        [SerializeField] private int restoreAmmo;
        [Header("Animation")]
        [SerializeField] private float durationRotate;
        [SerializeField] private float durationMove;
        [SerializeField] private float endValue;
        private HandWeapon handWeapon;
        private void Start()
        {
            AnimatedBonus();
        }

        public void TakeBonus()
        {
            handWeapon.WeaponStock = restoreAmmo;
        }

        public void AnimatedBonus()
        {
            var endPosition = transform.position.y + endValue;
            transform.DORotate(Vector3.up, durationRotate).SetLoops(-1, LoopType.Incremental);
            transform.DOMoveY(endPosition, durationMove).SetLoops(-1,LoopType.Yoyo);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.TryGetComponent<HandWeapon>(out handWeapon)) return;
            TakeBonus();
            Destroy(gameObject);
        }
    }
}
