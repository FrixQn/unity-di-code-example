using UnityEngine;

namespace DemoProject.Gameplay
{
    public class DamageDealer : CollectableObject
    {
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(Score);
                Destroy(gameObject);
            }
        }

        protected override void OnTriggerStay(Collider other)
        {
            base.OnTriggerStay(other);
        }
    }
}
