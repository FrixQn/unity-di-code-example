using System;
using UnityEngine;

namespace DemoProject.Gameplay
{
    public class BlackHoleCollision : MonoBehaviour
    {
        public event Action<Collider> Collided;

        private void OnTriggerEnter(Collider other)
        {
            Collided?.Invoke(other);
        }
    }
}
