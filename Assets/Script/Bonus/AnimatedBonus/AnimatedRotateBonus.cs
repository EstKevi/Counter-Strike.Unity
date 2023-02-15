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
            transform.DORotate(
                new Vector3(0, -360, 0),
                durationRotate,
                RotateMode.LocalAxisAdd
            ).SetLoops(-1)
            .SetEase(Ease.Linear);
            
            transform.DOMoveY(endPosition, durationMoveUp).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }
}