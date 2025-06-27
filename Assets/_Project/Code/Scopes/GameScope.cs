using DemoProject.Configs;
using DemoProject.Core;
using DemoProject.Gameplay;
using DemoProject.Presenters;
using DemoProject.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DemoProject.Scopes
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private SoundsController _soundsController;
        [SerializeField] private SoundsLibrary _soundsLibrary;
        [SerializeField] private CameraManager _cameraManager;
        [SerializeField] private ObjectsPool _objectsPool;
        [SerializeField] private GameplayConfig _gameplayConfig;
        [SerializeField] private ViewsContainer _viewsContainer;
        [SerializeField] private BlackHole _player;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_soundsController).As<ISoundsController>();
            builder.RegisterComponent(_cameraManager).As<ICameraManager>();
            builder.RegisterComponent(_inputManager).As<IInputManager>();
            builder.RegisterComponent(_objectsPool).As<IObjectsPool>();

            builder.RegisterComponent(_gameplayConfig).As<IGameplayConfig>();
            builder.Register<GameplayController>(Lifetime.Scoped).As<IGameplayController>();

            builder.RegisterEntryPoint<BlackHolePresenter>().AsSelf();
            builder.RegisterEntryPoint<GameplayPresenter>().AsSelf();
            builder.RegisterEntryPoint<CorePresenter>(Lifetime.Scoped);

            _viewsContainer.Configure(builder);

            builder.RegisterComponent(_player).As<IBlackHole>();
            builder.RegisterComponent(_soundsLibrary).AsSelf();
        }
    }
}
