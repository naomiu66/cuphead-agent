using BepInEx;
using BepInEx.Logging;
using CupheadRLPlugin.Collectors.PlayerCollector;
using CupheadRLPlugin.Collectors.EntitiesCollector;
using CupheadRLPlugin.Collectors.ProjectilesCollector;

namespace CupheadRLPlugin.Plugin
{
    [BepInPlugin("com.naomiu66.cupheadrl", "Cuphead RL", "0.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private PlayerCollector _playerCollector;
        private EntitiesCollector _entitiesCollector;
        private ProjectilesCollector _projectilesCollector;
        internal static new ManualLogSource? Logger;

        void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;
            Logger.LogInfo("Plugin is loaded");

            _playerCollector = new PlayerCollector(Logger);
            _entitiesCollector = new EntitiesCollector(Logger);
            _projectilesCollector = new ProjectilesCollector(Logger);
        }

        void Update()
        {
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