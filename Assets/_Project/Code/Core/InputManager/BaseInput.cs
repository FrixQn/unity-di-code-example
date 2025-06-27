using UnityEngine;

namespace DemoProject.Core
{
    public struct BaseInput : IInput
    {
        public Vector2 Joystick { get; set; }
    }
}
