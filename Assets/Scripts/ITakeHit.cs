public interface ITakeHit
{
    int Health { get; }

    void TakeHit(IDealDamage attacker);
}
