using BepInEx.Logging;

namespace CupheadRLPlugin.Collectors.ProjectilesCollector
{
    public class ProjectilesCollector
    {
        private readonly ManualLogSource _logger;

        private static readonly HashSet<string> EnemyProjectiles = new()
        {
            "EnemyProjectile",
            "Enemy"
        };

        public ProjectilesCollector(ManualLogSource logger)
        {
            _logger = logger;
        }

        public void Update()
        {
            var projectiles = UnityEngine.GameObject.FindObjectsOfType<AbstractProjectile>();

            var enemyProjectiles = new List<AbstractProjectile>();

            foreach (var projectile in projectiles)
            {
                if(EnemyProjectiles.Contains(projectile.tag))
                    enemyProjectiles.Add(projectile);
            }

            _logger.LogDebug($"[ProjectilesCollector] Found {projectiles.Length} projectiles");
            _logger.LogDebug($"[ProjectilesCollector] Found {enemyProjectiles.Count} enemy projectiles");
        }
    }
}