using Fantazee.Instance;
using FMODUnity;
using Fsi.Gameplay.Healths;

namespace Fantazee.Battle.Characters.Player
{
    public class BattlePlayer : BattleCharacter
    {
        public override Health Health => GameController.Instance.GameInstance.Character.Health;

        private CharacterInstance instance;
        
        // Audio
        protected override EventReference DeathSfxRef => instance.Data.DeathSfx;
        protected override EventReference EnterSfxRef => instance.Data.EnterSfx;

        public void Initialize(CharacterInstance character)
        {
            instance = character;
            SpawnVisuals(character.Data.Visuals);
            base.Initialize();
        }
    }
}