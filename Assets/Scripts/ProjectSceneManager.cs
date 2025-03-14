using System;
using Fantazee.Environments;
using Fantazee.Environments.Settings;
using Fsi.Gameplay.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fantazee
{
    public class ProjectSceneManager : FsiSceneManager<ProjectSceneManager>
    {
        [Header("Scenes")]
        
        [SerializeField]
        private FsiSceneEntry mainMenuScene;

        [SerializeField]
        private FsiSceneEntry battleScene;

        [SerializeField]
        private FsiSceneEntry bossScene;
        
        [SerializeField]
        private FsiSceneEntry mapScene;
        
        [SerializeField]
        private FsiSceneEntry blacksmithScene;

        [SerializeField]
        private FsiSceneEntry shopScene;

        [SerializeField]
        private FsiSceneEntry innScene;

        [SerializeField]
        private FsiSceneEntry encounterScene;

        public void LoadMainMenu(Action onComplete)
        {
            LoadSceneAsync(mainMenuScene.Name, LoadSceneMode.Single, onComplete);
        }

        public void LoadBattle(EnvironmentData environment, Action onComplete = null)
        {
            LoadSceneAsync(battleScene.Name, LoadSceneMode.Single, () => 
                                                                       LoadBattleEnvironment(environment, 
                                                                           onComplete));
        }
        
        public void LoadBossBattle(EnvironmentData environment, Action onComplete = null)
        {
            LoadSceneAsync(bossScene.Name, LoadSceneMode.Single, () => 
                                                                     LoadBattleEnvironment(environment, 
                                                                         onComplete));
        }

        public void LoadMap(EnvironmentData data, Action onComplete)
        {
            LoadSceneAsync(data.Map.Name, LoadSceneMode.Single, onComplete);
        }

        public void LoadBlacksmith(Action onComplete)
        {
            LoadSceneAsync(blacksmithScene.Name, LoadSceneMode.Single, onComplete);
        }

        public void LoadShop(Action onComplete)
        {
            LoadSceneAsync(shopScene.Name, LoadSceneMode.Single, onComplete);
        }

        public void LoadInn(Action onComplete)
        {
            LoadSceneAsync(innScene.Name, LoadSceneMode.Single, onComplete);
        }
        
        private void LoadBattleEnvironment(EnvironmentData data, Action onComplete)
        {
            LoadSceneAsync(data.Battle.Name, LoadSceneMode.Additive, onComplete);
        }

        public void LoadEncounter(Action onComplete)
        {
            LoadSceneAsync(encounterScene.Name, LoadSceneMode.Single, onComplete);
        }
    }
}
