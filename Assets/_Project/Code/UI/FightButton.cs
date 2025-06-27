using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DemoProject.UI
{
    public class FightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] private float _holdInterval;
        private float _holdTime = 0f;
        private bool _isHold;

        public event Action Cliked;

        public void OnPointerClick(PointerEventData eventData)
            => OnClick();

        public void OnPointerDown(PointerEventData eventData)
            =>  OnInteract(true);

        public void OnPointerUp(PointerEventData eventData)
            =>  OnInteract(false);

        private void Update()
        {
            if (!_isHold)
                return;

            _holdTime += Time.deltaTime;
            if (_holdTime >= _holdInterval)
                OnClick();
        }

        private void OnClick()
        {
            if (_isHold)
                Cliked?.Invoke();
            _holdTime = 0f;
        }

        public void OnInteract(bool isPressed)
        {
            _isHold = isPressed;
            _holdTime = 0f;
        }
    }
}
