using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DemoProject.UI
{
    public class ViewsContainer : MonoBehaviour
    {
        [SerializeField] private BlackHoleView _blackHoleView;
        [SerializeField] private GameplayViewController _gameplayView;

        public void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_blackHoleView).As<IBlackHoleView>();
            builder.RegisterComponent(_gameplayView).As<IGameplayView>();
        }
    }
}
