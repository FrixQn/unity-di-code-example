using UnityEngine;

namespace DemoProject.Gameplay
{
    public class Bullet : DamageDealer
    {
        [SerializeField] private LayerMask _excludeMaskWhenMove;
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void Move(Vector3 direction, float force)
        {
            _rb.interpolation = RigidbodyInterpolation.None;
            _rb.AddForce(direction * force);
            _rb.linearVelocity = Vector3.up * 10f;
            _rb.angularVelocity = Vector3.right * 45;
            _rb.detectCollisions = true;
            _rb.excludeLayers = _excludeMaskWhenMove;
        }
    }
}
