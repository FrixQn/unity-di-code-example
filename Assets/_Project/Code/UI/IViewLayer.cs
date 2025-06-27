namespace DemoProject.UI
{
    public interface IBaseUIObject
    {
        void SetVisible(bool isVisible);
    }

    public interface IViewLayer : IBaseUIObject
    {

    }

    public interface IGameplayViewLayer : IViewLayer
    {
        public GameplayViewLayerType LayerType { get; }
    }
}
