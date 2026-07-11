using BepInEx.Logging;

namespace CupheadRLPlugin.Collectors.EntitiesCollector
{
    public class EntitiesCollector
    {
        private readonly ManualLogSource _logger;

        public EntitiesCollector(ManualLogSource logger)
        {
            _logger = logger;
        }

        public void Update()
        {
            var entities = UnityEngine.GameObject.FindObjectsOfType<AbstractLevelEntity>();

            _logger.LogDebug($"[EntitiesCollector] Found {entities.Length} entities");
        }

    }
}