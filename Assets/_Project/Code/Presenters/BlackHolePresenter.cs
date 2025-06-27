using DemoProject.Gameplay;
using DemoProject.UI;
using System;
using UnityEngine;
using VContainer.Unity;

namespace DemoProject.Presenters
{
    public class BlackHolePresenter : IInitializable, IDisposable
    {
        private readonly IBlackHole _hole;
        private readonly IBlackHoleView _view;

        public void Initialize() { }

        public BlackHolePresenter(IBlackHole blackHole, IBlackHoleView view)
        {
            _hole = blackHole;
            _view = view;

            _hole.StageProgressChanged += OnHoleProgressChanged;
            _hole.SizeUpped += OnSizeUpped;
        }

        private void OnSizeUpped(int stage)
        {
            _view.ClearProgress();
            _view.SizeUpEffect(CalculateScaleFactorByStage(stage));
        }

        private void OnHoleProgressChanged(int score, int stage, float normalizedProgress)
        {
            _view.DisplayProgress(normalizedProgress);
            _view.FlashScore(score, CalculateScaleFactorByStage(stage));
        }

        private float CalculateScaleFactorByStage(int stage)
        {
            return Mathf.Clamp(stage, 1, float.MaxValue);
        }

        void IDisposable.Dispose()
        {
            _hole.StageProgressChanged -= OnHoleProgressChanged;
            _hole.SizeUpped -= OnSizeUpped;
        }
    }
}
