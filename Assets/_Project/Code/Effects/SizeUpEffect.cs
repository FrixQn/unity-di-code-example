using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace DemoProject.Effects
{
    [RequireComponent(typeof(TextMeshPro))]
    public class SizeUpEffect : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _text;
        [SerializeField] private bool _startOnAwake = true;
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _moveDir;
        private Vector3 _textDefaultPosition;
        private Action<SizeUpEffect> _onComplete;
        private Sequence _sequence;

        public string Text { get => _text.text; set { _text.text = value; } }
        public float Duration { get => _duration; set { _duration = value; } }

        private void Awake()
        {
            if (_startOnAwake)
                Play();
        }

        public void Play(bool destroyAfterComplete = false)
        {
            Play(Text, Duration, destroyAfterComplete);
        }

        public void Play(string text, float duration, bool destroyAfterComplete = false, Action<SizeUpEffect> onComplete = null)
        {
            _sequence?.Kill();
            _sequence = null;
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1f);
            _onComplete = onComplete;

            Text = text;
            Duration = duration;

            _sequence = DOTween.Sequence().
                Insert(0f, _text.DOFade(0f, _duration)).
                Insert(0f, _text.transform.DOMove(_moveDir, _duration).SetRelative());

            if (destroyAfterComplete)
                _onComplete += DestroyAfterCompleted;

            _sequence.onComplete = OnCompleted;

        }

        private void Update()
        {
            _text.transform.rotation = Camera.main.transform.rotation;
        }

        private void OnCompleted()
        {
            _onComplete?.Invoke(this);
        }

        private void DestroyAfterCompleted(SizeUpEffect effect)
        {
            Destroy(effect.gameObject);
        }
    }
}
