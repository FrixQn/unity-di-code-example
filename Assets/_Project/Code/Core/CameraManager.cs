using Unity.Cinemachine;
using UnityEngine;

namespace DemoProject.Core
{
    public class CameraManager : MonoBehaviour, ICameraManager
    {
        private const int HIGHEST_PRIORITY_VALUE = 0;
        private const int LOWEST_PRIORITY_VALUE = HIGHEST_PRIORITY_VALUE - 1;

        [SerializeField] private CinemachineCamera _blackHoleCamera;
        [SerializeField] private CinemachineCamera _bossLookCamera;

        private void Awake()
        {
            SetBlackHoleCameraAsMain();
        }

        public void SetBlackHoleCameraAsMain()
        {
            _blackHoleCamera.Priority = HIGHEST_PRIORITY_VALUE;
            _bossLookCamera.Priority = LOWEST_PRIORITY_VALUE;
        }

        public void SetBossCameraAsMain()
        {
            _bossLookCamera.Priority = HIGHEST_PRIORITY_VALUE;
            _blackHoleCamera.Priority = LOWEST_PRIORITY_VALUE;
        }

        public void IncreaseBlackHoleTransposerDistance(float increment)
        {
            if (_blackHoleCamera.GetCinemachineComponent(CinemachineCore.Stage.Body) is CinemachinePositionComposer composer)
                composer.CameraDistance += increment;
        }

        public void IncreaseBossHoleTransposerDistance(float increment)
        {
            if (_bossLookCamera.GetCinemachineComponent(CinemachineCore.Stage.Body) is CinemachinePositionComposer composer)
                composer.CameraDistance += increment;
        }
    }
}