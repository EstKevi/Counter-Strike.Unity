using Script.player.Player.heal;
using UnityEngine;

public class Heals : MonoBehaviour, IDamageable
{
    [SerializeField] private float heal;
    
    public void Apply(float damage)
    {
        heal =- damage;
    }
}