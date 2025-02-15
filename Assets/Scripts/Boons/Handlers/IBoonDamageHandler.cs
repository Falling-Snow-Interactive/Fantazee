using ProjectYahtzee.Battle;

namespace ProjectYahtzee.Boons.Handlers
{
    /// <summary>
    /// For boons that modify the damage. A Damage object is modified as its passed through the boons.
    /// </summary>
    public interface IBoonDamageHandler
    {
        public Damage ReceiveDamage(Damage damage);
        
        public Boon Boon { get; }
    }
}