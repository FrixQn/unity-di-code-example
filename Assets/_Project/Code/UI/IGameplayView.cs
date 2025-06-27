using System.Threading.Tasks;

namespace DemoProject.UI
{
    public interface IGameplayView : IBaseUIObject
    {
        Task LevelCompleteSequence();
        void ToggleScreen(GameplayViewLayerType screenType, out BaseGameplayViewLayer layer);
        void ToggleScreen(GameplayViewLayerType screenType);
        bool TryGetLayer<T>(out T view) where T : BaseGameplayViewLayer;
    }
}
