using DemoProject.Gameplay;
using DemoProject.UI;
using VContainer.Unity;

namespace DemoProject.Presenters
{
    public class GameplayPresenter : IInitializable, IStartable
    {
        private readonly IGameplayView _view;
        private readonly IGameplayController _gameplayController;

        public GameplayPresenter(IGameplayView view, IGameplayController gameplayController)
        {
            _view = view;
            _gameplayController = gameplayController;

            _gameplayController.ScoreChanged += OnScoreChanged;
            _gameplayController.LevelCompleted += OnLevelCompleted;
            _gameplayController.EnemyDead += OnEnemyDead;

            
            _view.SetVisible(true);
        }

        public void Initialize()
        {
            _gameplayController.StartGame();
        }

        public void Start()
        {
            if (_view.TryGetLayer(out FightGameplayLayer fightLayer))
            {
                fightLayer.FightButtonClicked += OnFireButtonClicked;
            }

            if (_view.TryGetLayer(out PopupGameplayLayer popupLayer))
            {
                popupLayer.ConfirmButtonClicked += RestartGame;
            }
        }

        private void RestartGame()
        {
            _gameplayController.RestartGame();
        }

        private void OnEnemyDead()
        {
            _view.ToggleScreen(GameplayViewLayerType.Popup, out var layer);
            if (layer is PopupGameplayLayer popupLayer)
                popupLayer.DisplayMessage("Level completed");
        }

        private void OnFireButtonClicked()
            => _gameplayController.Shoot();

        private async void OnLevelCompleted()
        {
            if (_view.TryGetLayer(out MainGameplayLayer view))
                view.SetJoystickVisible(false);
            await _view.LevelCompleteSequence();
            _view.ToggleScreen(GameplayViewLayerType.Fight);
        }

        private void OnScoreChanged(int current, int required)
        {
            if (_view.TryGetLayer(out MainGameplayLayer view))
                view.DisplayProgress(current, required);
        }
    }
}
