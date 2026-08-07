using System.Security.Permissions;
using BepInEx;
using BepInEx.Logging;
using CupheadRLPlugin.Models.PlayerState;
using UnityEngine;

namespace CupheadRLPlugin.Collectors.PlayerCollector
{
    public class PlayerCollector
    {
        private AbstractPlayerController? _playerController;
        private LevelPlayerMotor? _levelPlayerMotor;
        private int HP;
        public bool Ready => _playerController != null && _levelPlayerMotor != null;
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

        void TryGetMotor()
        {
            if (_levelPlayerMotor == null)
            {
                _levelPlayerMotor = UnityEngine.Object.FindObjectOfType<LevelPlayerMotor>();

                if (_levelPlayerMotor != null)
                {
                    _logger.LogInfo("[PlayerCollector] LevelPlayerMotor found");
                }
            }
        }

        public void TryInitialize()
        {
            TryGetController();
            TryGetMotor();
        }

        public void Collect(PlayerState state)
        {
            if (!Ready) return;

            state.HP = HP;
            state.PosX = _playerController!.transform.position.x;
            state.PosY = _playerController!.transform.position.y;
            state.Dashing = _levelPlayerMotor!.Dashing;
            state.Grounded = _levelPlayerMotor!.Grounded;
            state.Locked = _levelPlayerMotor.Locked;
        }


        // TODO: Delete update method
        public void Update()
        {
            TryInitialize();

            if (!Ready) return;

            _logger.LogInfo($"[PlayerCollector] grounded: {_levelPlayerMotor!.Grounded}");
            _logger.LogInfo($"[PlayerCollector] dashing: {_levelPlayerMotor!.Dashing}");
        }

        void OnHealthChanged(int health, PlayerId playerId)
        {
            _logger.LogDebug($"[PlayerCollector] Damage received, Health = {health}, playerId = {playerId}");
            HP = health;
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