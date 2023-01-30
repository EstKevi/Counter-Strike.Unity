using Script.player.Player.heal;
using UnityEngine;

public class Heals : MonoBehaviour, IDamageable
{
    [SerializeField] private int heal;
    public int Heal => heal;
    public void Apply(int damage)
    {
        Debug.Log($"Damage: {damage}");
        heal -= damage;
        if (heal < 0)
            heal = 0;
    }
}