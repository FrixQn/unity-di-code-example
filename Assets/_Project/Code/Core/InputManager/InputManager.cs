using UnityEngine;
using UnityEngine.InputSystem;

namespace DemoProject.Core
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        private BaseInput _input;
        public IInput GetInput() => _input;

        private void OnMove(InputValue value)
        {
            _input.Joystick = value.Get<Vector2>();
        }
    }
}
