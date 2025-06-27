using DemoProject.Core;
using DemoProject.Effects;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DemoProject.UI
{
    public interface IBlackHoleView
    {
        void ClearProgress();
        void DisplayProgress(float normalizedProgress);
        void SizeUpEffect(float scaleFactor);
        void FlashScore(int value, float scaleFactor);
    }

    public class BlackHoleView : MonoBehaviour, IBlackHoleView
    {
        private const float DEFAULT_DURATION = .1f;
        private const float ZERO_FILL_AMOUNT_DURATION = .5f;

        [SerializeField] private Image _image;
        [SerializeField] private SizeUpEffect _sizeUpEffect;
        private Tween _progressTween;
        private IObjectsPool _pool;

        [Inject]
        private void Inject(IObjectsPool objectsPool)
        {
            _pool = objectsPool;
        }

        public void DisplayProgress(float normalizedProgress)
        {
            _progressTween?.Kill();
            _progressTween = _image.DOFillAmount(normalizedProgress, GetDurationByTargetFillAmount(normalizedProgress));
        }

        public void FlashScore(int value, float scaleFactor)
        {
            var effect = _pool.Get(_sizeUpEffect);
            effect.transform.localScale *= scaleFactor;
            effect.gameObject.transform.position = transform.position;
            effect.Play($"+{value}", 0.3f, false, OnScoreEffectCompleted);
        }

        private void OnScoreEffectCompleted(SizeUpEffect effect)
        {
            _pool.BackToPool(_sizeUpEffect.gameObject, effect.gameObject);
        }

        public void SizeUpEffect(float scaleFactor)
        {
            var effect = _pool.Get(_sizeUpEffect);
            effect.transform.localScale *= scaleFactor;
            effect.gameObject.transform.position = transform.position;
            effect.Play($"Size Up", 0.3f, false, OnSizeUpEffectCompleted);
        }

        private void OnSizeUpEffectCompleted(SizeUpEffect effect)
        {
            _pool.BackToPool(_sizeUpEffect.gameObject, effect.gameObject);
        }

        public void ClearProgress()
        {
            if (_progressTween != null)
            {
                _progressTween.onComplete += ClearProgressCallback;
            }
        }

        private void ClearProgressCallback()
        {
            _progressTween.onComplete -= ClearProgressCallback;
            _progressTween = _image.DOFillAmount(0f, GetDurationByTargetFillAmount(0f));
        }

        private float GetDurationByTargetFillAmount(float value)
        {
            return value > 0 ? DEFAULT_DURATION : ZERO_FILL_AMOUNT_DURATION;
        }
    }
}
