using Fantazee.Battle;

namespace Fantazee.Boons.Handlers
{
    /// <summary>
    /// For boons that modify the damage. A Damage object is modified as its passed through the boons.
    /// </summary>
    public interface IBoonDamageHandler
    {
        public Boon Boon { get; }
        public void ReceiveDamage(ref Damage damage);
    }
}