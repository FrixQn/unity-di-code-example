using VContainer;
using VContainer.Unity;

namespace DemoProject.Presenters
{
    public class CorePresenter : IInitializable
    {
        private readonly GameplayPresenter _gameplayPresenter;
        private readonly BlackHolePresenter _blackHolePresenter;

        [Inject]
        public CorePresenter(GameplayPresenter gameplayPresenter, BlackHolePresenter blackHolePresenter)
        {
            _gameplayPresenter = gameplayPresenter;
            _blackHolePresenter = blackHolePresenter;
        }

        public void Initialize() { }
    }
}
