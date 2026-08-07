using BepInEx;
using BepInEx.Logging;
using CupheadRLPlugin.Collectors.PlayerCollector;
using CupheadRLPlugin.Collectors.EntitiesCollector;
using CupheadRLPlugin.Collectors.ProjectilesCollector;
using UnityEngine;
using UnityEngine.SceneManagement;
using CupheadRLPlugin.Models.GameState;

namespace CupheadRLPlugin.Plugin
{
    [BepInPlugin("com.naomiu66.cupheadrl", "Cuphead RL", "0.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private PlayerCollector _playerCollector = null!;
        private EntitiesCollector _entitiesCollector = null!;
        private ProjectilesCollector _projectilesCollector = null!;
        private GameState gameState = new();
        internal static new ManualLogSource Logger = null!;

        void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;
            Logger.LogInfo("Plugin is loaded");

            _playerCollector = new PlayerCollector(Logger);
            _entitiesCollector = new EntitiesCollector(Logger);
            _projectilesCollector = new ProjectilesCollector(Logger);

            SceneLoader.LoadScene(Scenes.scene_level_veggies, SceneLoader.Transition.None, SceneLoader.Transition.None);
        }

        void Start()
        {
            // SceneLoader.LoadLevel(Levels.Veggies, SceneLoader.Transition.None);
        }

        void Update()
        {
            _playerCollector.TryInitialize();

            if (_playerCollector.Ready) _playerCollector.Collect(gameState.playerState)
            _playerCollector.Update();
            _entitiesCollector.Update();
            _projectilesCollector.Update();
        }

        void OnDestroy()
        {
            _playerCollector.OnDestroy();
        }
    }
}