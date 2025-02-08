using Fsi.Gameplay.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private FsiSceneEntry battleEnv;
        
        public void LoadBattle()
        {
            LoadScene(battleScene.Name, LoadSceneMode.Single);
        }

        public void LoadMap()
        {
            LoadScene(mapScene.Name, LoadSceneMode.Single);
        }

        public void LoadBattleEnvironment()
        {
            LoadScene(battleEnv.Name, LoadSceneMode.Additive);
        }
    }
}
