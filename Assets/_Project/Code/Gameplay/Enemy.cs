using UnityEngine;

namespace DemoProject.Gameplay
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private float _health;
        public float Health => _health;
        public bool IsAlive => _health > 0;

        public event OnDamagedDelegate Damaged;

        public void Initialize(float health)
        {
            _health = health;
        }

        public void TakeDamage(float damage)
        {
            if (!IsAlive)
                return;

            if (damage <= 0)
                return;

            _health -= damage;
            _health = Mathf.Clamp(_health, 0, int.MaxValue);
            OnDamaged(damage);
        }

        private void OnDamaged(float damage)
        {
            Damaged?.Invoke(this, damage);
        }
    }
}
