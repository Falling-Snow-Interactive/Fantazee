using System;
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
        private FsiSceneEntry mapScene;
        
        [SerializeField]
        private FsiSceneEntry blacksmithScene;

        [SerializeField]
        private FsiSceneEntry shopScene;

        [SerializeField]
        private FsiSceneEntry innScene;
        
        [Header("Battle Environments")]
        
        [FormerlySerializedAs("sandboxEnv")]
        [SerializeField]
        private FsiSceneEntry environmentEnv;

        [SerializeField]
        private FsiSceneEntry grassEnv;

        public void LoadMainMenu(Action onComplete)
        {
            LoadSceneAsync(mainMenuScene.Name, LoadSceneMode.Single, onComplete);
        }
        
        public void LoadBattle(Action onComplete)
        {
            LoadSceneAsync(battleScene.Name, LoadSceneMode.Single, () =>
                                                                   {
                                                                       LoadGrassEnvironment(onComplete);
                                                                   });
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
        
        public void LoadBattleEnvironment(Action onComplete)
        {
            LoadSceneAsync(environmentEnv.Name, LoadSceneMode.Additive, onComplete);
        }
        
        public void LoadGrassEnvironment(Action onComplete)
        {
            LoadSceneAsync(grassEnv.Name, LoadSceneMode.Additive, onComplete);
        }
    }
}
