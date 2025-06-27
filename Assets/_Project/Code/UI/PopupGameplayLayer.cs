using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DemoProject.UI
{
    public class PopupGameplayLayer : BaseGameplayViewLayer
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _confirm;

        public event Action ConfirmButtonClicked;

        public void DisplayMessage(string message)
            => _text.text = message;

        protected override void Subscribe()
            =>  _confirm.onClick.AddListener(OnConfirm);

        private void OnConfirm()
            => ConfirmButtonClicked?.Invoke();

        protected override void Unsubscribe()
           =>  _confirm.onClick.RemoveAllListeners();
    }
}
