namespace Muramasa.Player
{
    public interface IActor
    {
        int Health { get; }

        void TakeDamage(int damage);
    }
}