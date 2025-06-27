namespace DemoProject.Core
{
    public interface ICameraManager
    {
        void SetBossCameraAsMain();
        void SetBlackHoleCameraAsMain();
        void IncreaseBlackHoleTransposerDistance(float increment);
        void IncreaseBossHoleTransposerDistance(float increment);
    }
}
