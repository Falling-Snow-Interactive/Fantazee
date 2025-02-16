using Fsi.Gameplay.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace ProjectYahtzee
{
    public class ProjectSceneManager : FsiSceneManager<ProjectSceneManager>
    {
        [Header("Scenes")]

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
        
        public void LoadBattle()
        {
            LoadScene(battleScene.Name, LoadSceneMode.Single);
            LoadBattleEnvironment();
        }

        public void LoadMap()
        {
            LoadScene(mapScene.Name, LoadSceneMode.Single);
        }

        public void LoadBattleEnvironment()
        {
            LoadScene(sandboxEnv.Name, LoadSceneMode.Additive);
        }

        public void LoadBlacksmith()
        {
            LoadScene(blacksmithScene.Name, LoadSceneMode.Single);
        }

        public void LoadShop()
        {
            LoadScene(shopScene.Name, LoadSceneMode.Single);
        }

        public void LoadInn()
        {
            LoadScene(innScene.Name, LoadSceneMode.Single);
        }
    }
}
