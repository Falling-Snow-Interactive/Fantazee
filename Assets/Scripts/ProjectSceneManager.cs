using System;
using Fsi.Gameplay.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fantahzee
{
    public class ProjectSceneManager : FsiSceneManager<ProjectSceneManager>
    {
        [Header("Scenes")]
        
        [SerializeField]
        private FsiSceneEntry mainMenuScene;

        [SerializeField]
        private FsiSceneEntry battleScene;
        
        [SerializeField]
        private FsiSceneEntry mapScene;

        [SerializeField]
        private FsiSceneEntry sandboxEnv;

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
        
        public void LoadBattle(Action onComplete)
        {
            LoadSceneAsync(battleScene.Name, LoadSceneMode.Single, () =>
                                                                   {
                                                                       LoadBattleEnvironment(onComplete);
                                                                   });
        }

        public void LoadMap(Action onComplete)
        {
            LoadSceneAsync(mapScene.Name, LoadSceneMode.Single, onComplete);
        }

        public void LoadBattleEnvironment(Action onComplete)
        {
            LoadSceneAsync(sandboxEnv.Name, LoadSceneMode.Additive, onComplete);
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
    }
}
