using UnityEngine;

namespace DemoProject.UI
{
    public class BaseGameplayViewLayer : BaseViewLayer, IGameplayViewLayer
    {
        [field: SerializeField] public GameplayViewLayerType LayerType { get; private set; }
    }
}
