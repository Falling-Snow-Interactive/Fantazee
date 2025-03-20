namespace Fantazee.Battle.Characters.Intentions
{
    public class Intention
    {
        public IntentionType Type { get; private set; }
        public int Amount { get; private set; }

        public Intention(IntentionType type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}