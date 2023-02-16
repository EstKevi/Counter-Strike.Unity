using System;
using Script.Bonus.BonusCore;
using Script.Other;
using Script.player;
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
            if(GetComponent<Collider>().EnsureNotNull().isTrigger) return;
            throw new Exception("Must be \"Trigger\"");
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.TryGetComponent<Player>(out _)) return;
            if (bonusType.ApplyBonus(other.gameObject))
            {
                Destroy(gameObject);
            }
        }
    }
}