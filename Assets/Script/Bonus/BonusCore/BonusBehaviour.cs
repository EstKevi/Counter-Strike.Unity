using UnityEngine;

namespace Script.Bonus.BonusCore
{
    public abstract class BonusBehaviour : MonoBehaviour, IBonus
    {
        public abstract bool ApplyBonus(GameObject obj);
    }
}