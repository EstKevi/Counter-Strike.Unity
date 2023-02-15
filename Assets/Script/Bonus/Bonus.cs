using System;
using Script.Other;
using Script.player.Bonus;
using UnityEngine;

namespace Script.Bonus
{
    [RequireComponent(typeof(Collider))]
    public class Bonus : MonoBehaviour
    {
        [SerializeField] private BonusBehaviour bonusType;
        private void Awake()
        {
            bonusType.EnsureNotNull();
            
            if(!GetComponent<Collider>().EnsureNotNull().isTrigger)
            {
                throw new Exception("Must be \"Trigger\"");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IApplyBonus>(out var useBonus)) return;

            if (useBonus.ApplyBonus(bonusType))
            {
                Destroy(gameObject);
            }
        }
    }
}