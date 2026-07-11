using BepInEx;
using BepInEx.Logging;

namespace CupheadRLPlugin.Collectors.PlayerCollector
{
    public class PlayerCollector
    {
        private AbstractPlayerController? _playerController;
        private readonly ManualLogSource _logger;

        public PlayerCollector(ManualLogSource logger)
        {
            _logger = logger;
        }

        void TryGetController()
        {
            if (_playerController == null)
            {
                _playerController = UnityEngine.Object.FindObjectOfType<AbstractPlayerController>();

                if (_playerController != null)
                {
                    var playerStats = _playerController.stats;
                    playerStats.OnHealthChangedEvent += OnHealthChanged;
                    _logger.LogInfo("[PlayerCollector] PlayerController found");
                    _logger.LogInfo("[PlayerCollector] Subscribed OnHealthChangedEvent");   
                }
            }
        }

        public void Update()
        {
            TryGetController();
        }

        void OnHealthChanged(int health, PlayerId playerId)
        {
            _logger.LogDebug($"[PlayerCollector] Damage received, Health = {health}, playerId = {playerId}");
        }

        public void OnDestroy()
        {
            if(_playerController != null)
            {
                _playerController.stats.OnHealthChangedEvent -= OnHealthChanged;
            }
        }
    }
}