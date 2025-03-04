using Fantazee.Instance;
using Fantazee.Relics.Data;

namespace Fantazee.Relics.Instance
{
    public class LuckyRelicInstance : RelicInstance
    {
        public LuckyRelicInstance(RelicData data, CharacterInstance character) : base(data, character)
        {
            character.Rolls++;
        }
    }
}