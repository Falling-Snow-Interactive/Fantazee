using System;
using Fantazee.Battle.Environments;
using Fantazee.Environments;
using Fantazee.Environments.Information;
using Fantazee.Environments.Settings;
using Fsi.Gameplay.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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

        public void LoadMainMenu(Action onComplete)
        {
            LoadSceneAsync(mainMenuScene.Name, LoadSceneMode.Single, onComplete);
        }
        
        public void LoadBattle(EnvironmentType environmentType, Action onComplete = null)
        {
            LoadSceneAsync(battleScene.Name, LoadSceneMode.Single, () => 
                                                                       LoadBattleEnvironment(environmentType, 
                                                                           onComplete));
        }
        
        public void LoadBossBattle(EnvironmentType environmentType, Action onComplete = null)
        {
            LoadSceneAsync(bossScene.Name, LoadSceneMode.Single, () => 
                                                                     LoadBattleEnvironment(environmentType, 
                                                                         onComplete));
        }

        public void LoadMap(Action onComplete)
        {
            LoadSceneAsync(mapScene.Name, LoadSceneMode.Single, onComplete);
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

        private void LoadBattleEnvironment(EnvironmentType env, Action onComplete)
        {
            if (EnvironmentSettings.Settings.TryGetEnvironment(env, out EnvironmentData data))
            {
                LoadSceneAsync(data.Scene.Name, LoadSceneMode.Additive, onComplete);
            }
        }
    }
}
