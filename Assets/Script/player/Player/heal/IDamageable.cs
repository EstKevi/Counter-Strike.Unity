namespace Script.player.Player.heal
{
    public interface IDamageable
    {
        void ApplyDamage(int damage);

        void ApplyHeal(int regenHeal);
    }
}