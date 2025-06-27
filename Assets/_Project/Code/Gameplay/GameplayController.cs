using DemoProject.Configs;
using DemoProject.Core;
using UnityEngine;

namespace DemoProject.Gameplay
{
    public class GameplayController : IGameplayController, System.IDisposable
    {
        private readonly IBlackHole _player;
        private readonly ICameraManager _cameraManager;
        private readonly ISoundsController _soundsController;
        private readonly IGameplayConfig _config;
        private int _score = 0;

        public event ScoreChangedDelegate ScoreChanged;
        public event LevelCompletedDelegate LevelCompleted;
        public event EnemyDeadDelegate EnemyDead;

        public GameplayController(IBlackHole player, ISoundsController soundsController, ICameraManager cameraManager, IGameplayConfig config)
        {
            _config = config;
            _player = player;
            _soundsController = soundsController;
            _cameraManager = cameraManager;
        }

        public void RestartGame()
        {
            throw new System.NotImplementedException();
        }

        public void StartGame()
        {
            _score = 0;
            _player.Initialize(_config.PlayerSpeed, _config.PlayerShootForce, new Bounds(_config.MapCenter, _config.MapSize));
            _player.ObjectFalled += OnObjectFalled;
            _player.SizeUpped += OnSizeUpped;
        }

        public void Shoot()
        {
            _player.Shoot();
        }

        private void OnSizeUpped(int stage)
        {
            _cameraManager.IncreaseBlackHoleTransposerDistance(7.5f);
        }

        private void OnObjectFalled(int score)
        {
            _score += score;
            ScoreChanged?.Invoke(_score, _config.ScoreToComplete);
            if (_score >= _config.ScoreToComplete)
            {
                OnComplete();
            }
        }

        private void OnComplete()
        {
            _soundsController.PlaySound("LevelComplete");
            _cameraManager.SetBossCameraAsMain();
            LevelCompleted?.Invoke();

            var enemy = Object.Instantiate(_config.EnemyPrefab, new Vector3(_player.Position.x, 0f, _player.Position.z) + _config.EnemySpawnOffset, 
                Quaternion.Euler(_config.EnemyDefaultRotation));
            enemy.Initialize(_config.ScoreToComplete);

            enemy.Damaged += OnEnemyDamaged;
        }

        private void OnEnemyDamaged(IDamageable enemy, float damage)
        {
            if (!enemy.IsAlive)
            {
                EnemyDead?.Invoke();
                Object.Destroy(((Enemy)enemy).gameObject);
            }
        }

        public void Dispose()
        {
            _player.ObjectFalled -= OnObjectFalled;
            _player.SizeUpped -= OnSizeUpped;
        }
    }
}
