using System;
using UnityEngine;

namespace DemoProject.UI
{
    public class FightGameplayLayer : BaseGameplayViewLayer
    {
        [SerializeField] private FightButton _fightButton;

        public event Action FightButtonClicked;

        protected override void Subscribe()
            =>  _fightButton.Cliked += OnClicked;

        private void OnClicked()
            => FightButtonClicked?.Invoke();

        protected override void Unsubscribe()
            => _fightButton.Cliked -= OnClicked;
    }
}
