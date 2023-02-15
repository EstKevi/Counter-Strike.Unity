using DG.Tweening;
using UnityEngine;

namespace Script.Bonus.AnimatedBonus
{
    public class AnimatedRotateBonus : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private float durationRotate;
        [SerializeField] private float durationMoveUp;
        [SerializeField] private float endValue;

        private void Start()
        {
            var endPosition = transform.position.y + endValue;
            transform.DORotate(Vector3.up, durationRotate).SetLoops(-1, LoopType.Incremental);
            transform.DOMoveY(endPosition, durationMoveUp).SetLoops(-1, LoopType.Yoyo);
        }
    }
}