using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DemoProject.UI
{
    public class GameplayViewController : BaseUIObject, IGameplayView
    {
        [SerializeField] private GameplayViewLayerType _defaultLayer;
        [SerializeField] private BaseGameplayViewLayer[] _views;
        private Dictionary<GameplayViewLayerType, BaseGameplayViewLayer> _view = new ();

        private void Awake()
        {
            foreach (var view in _views)
            {
                _view.Add(view.LayerType, view);
            }
        }

        private T GetView<T>(GameplayViewLayerType screenType) where T : BaseViewLayer
        {
            return _view[screenType] as T;
        }

        public async Task LevelCompleteSequence()
        {
            await GetView<MainGameplayLayer>(GameplayViewLayerType.Main).LevelCompleteSequence();
        }

        public void ToggleScreen(GameplayViewLayerType screenType)
        {
            foreach(var view in _view)
            {
                if (view.Key == screenType)
                    view.Value.SetVisible(true);
                else
                    view.Value.SetVisible(false);
            }
        }

        public void DisplayMessage(string message)
        {
            (_view[GameplayViewLayerType.Popup] as PopupGameplayLayer).DisplayMessage(message);
        }

        public void SetJoystickVisible(bool isVisible)
        {
            GetView<MainGameplayLayer>(GameplayViewLayerType.Main).SetJoystickVisible(isVisible);
        }

        public override void SetVisible(bool isVisible)
        {
            if (isVisible)
                ToggleScreen(_defaultLayer);
            else
                HideAllScreens();
        }

        private void HideAllScreens()
        {
            foreach (var view in _view)
            {
                view.Value.SetVisible(false);
            }
        }

        public void ToggleScreen(GameplayViewLayerType screenType, out BaseGameplayViewLayer layer)
        {
            layer = null;
            foreach (var view in _view)
            {
                if (view.Key == screenType)
                    layer = view.Value;

                view.Value.SetVisible(view.Key == screenType);
            }
        }

        public bool TryGetLayer<T>(out T view) where T : BaseGameplayViewLayer
        {
            view = default;
            foreach(var layer in _view)
            {
                if (layer.Value.GetType() == typeof(T))
                {
                    view = (T)layer.Value;
                    return true;
                }
            }

            return false;
        }
    }
}
