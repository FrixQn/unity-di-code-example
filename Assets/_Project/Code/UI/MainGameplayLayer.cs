using DG.Tweening;
using FrixQn.SimpleJoystick;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DemoProject.UI
{
    public class MainGameplayLayer : BaseGameplayViewLayer
    {
        private const float PROGRESS_VALUE_DURATION = 0.1f;
        [SerializeField] private SimpleJoystick _joystick;
        [SerializeField] private Slider _slider;
        [SerializeField] private CanvasGroup _sliderGroup;
        [SerializeField] private BossFightAnim _bossFightAnim;
        [SerializeField] private GameObject _confetti;
        [SerializeField] private float _confettiDuration;

        public void DisplayProgress(int current, int max)
        {
            _slider.minValue = 0;
            _slider.maxValue = max;
            _slider.DOValue(Mathf.Clamp(current, 0, max), PROGRESS_VALUE_DURATION);
        }

        public async Task LevelCompleteSequence()
        {
            _confetti.SetActive(true);
            await _sliderGroup.DOFade(0f, _confettiDuration).AsyncWaitForCompletion();
            _confetti.SetActive(false);
            await _bossFightAnim.Play();
        }

        public void SetJoystickVisible(bool isVisible)
        {
            _joystick.gameObject.SetActive(isVisible);
        }
    }
}
