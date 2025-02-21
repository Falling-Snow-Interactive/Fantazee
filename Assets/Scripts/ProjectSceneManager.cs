using System;
using Fantazee.Battle.Environments;
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

        [FormerlySerializedAs("grassEnv")]
        [SerializeField]
        private FsiSceneEntry woodsEnv;

        public void LoadMainMenu(Action onComplete)
        {
            LoadSceneAsync(mainMenuScene.Name, LoadSceneMode.Single, onComplete);
        }
        
        public void LoadBattle(EnvironmentType environmentType, Action onComplete = null)
        {
            LoadSceneAsync(battleScene.Name, LoadSceneMode.Single, () =>
                                                                   {
                                                                       switch (environmentType)
                                                                       {
                                                                           case EnvironmentType.Woods:
                                                                               LoadWoodsEnvironment(onComplete);
                                                                               break;
                                                                           case EnvironmentType.Plains:
                                                                               break;
                                                                           case EnvironmentType.Beach:
                                                                               break;
                                                                           case EnvironmentType.Mountains:
                                                                               break;
                                                                           case EnvironmentType.Volcano:
                                                                               break;
                                                                           default:
                                                                               throw new ArgumentOutOfRangeException(nameof(environmentType), environmentType, null);
                                                                       }
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

        private void LoadWoodsEnvironment(Action onComplete)
        {
            LoadSceneAsync(woodsEnv.Name, LoadSceneMode.Additive, onComplete);
        }
    }
}
