
using Fantazee.Audio;
using Fantazee.Environments;
using Fantazee.Environments.Settings;
using Fantazee.Instance;

namespace Fantazee.Battle.Boss
{
    public class BossBattleController : BattleController
    {
        protected override void PlayMusic()
        {
            if (EnvironmentSettings.Settings.TryGetEnvironment(GameInstance.Current.Environment.Data.Type,
                                                               out EnvironmentData data))
            {
                MusicController.Instance.PlayMusic(data.BossMusic);
            }
        }
        
        protected override void BattleWin()
        {
            base.BattleWin();

            GameController.Instance.GameInstance.Environment.SetReadyToAdvance();
        }
    }
}