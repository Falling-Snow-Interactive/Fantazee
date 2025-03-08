using Fantazee.Instance;
using Fantazee.Relics.Data;

namespace Fantazee.Relics.Instance
{
    public class LuckyRelicInstance : RelicInstance
    {
        public LuckyRelicInstance(RelicData data, CharacterInstance character) : base(data, character)
        {
        }

        public override void Enable()
        {
            character.Rolls++;
        }

        public override void Disable()
        {
            character.Rolls--;
        }
    }
}