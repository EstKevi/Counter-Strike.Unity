using System.Threading;
using DG.Tweening;
using Script.player.Player.heal;
using UnityEngine;

namespace Script.Bonus
{
    public class HealBonus : MonoBehaviour, IBonus
    {
        private Heals heal;
        [SerializeField] private int restoreHealth;
        [Header("Animation")]
        [SerializeField] private float durationRotate;
        [SerializeField] private float durationMove;
        [SerializeField] private float endValue;
        private void Start()
        {
            AnimatedBonus();
        }

        public void TakeBonus()
        {
            heal.ApplyHeal(restoreHealth);
            heal = null;
        }

        public void AnimatedBonus()
        {
            var endPosition = transform.position.y + endValue;
            transform.DORotate(Vector3.up, durationRotate).SetLoops(-1, LoopType.Incremental);
            transform.DOMoveY(endPosition, durationMove).SetLoops(-1,LoopType.Yoyo);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Heals>(out heal))
            {
                TakeBonus();
                Destroy(gameObject);
            }
        }
    }
}