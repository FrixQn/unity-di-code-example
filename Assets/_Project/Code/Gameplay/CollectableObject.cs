using UnityEngine;

namespace DemoProject.Gameplay
{
    public interface ICollectableObject
    {
        public int Score { get; }
        public bool IsCollected { get; }
        public GameObject GameObject { get; }
        public void Collect();
    }

    [RequireComponent(typeof(Rigidbody))]
    public class CollectableObject : MonoBehaviour, ICollectableObject
    {
        [field: SerializeField] public int Score { get; private set; }
        [SerializeField] private LayerMask _groundLayer;
        protected Rigidbody _rb;
        private Collider _collider;

        public bool IsCollected { get; private set; }
        public GameObject GameObject => gameObject;

        private void Awake()
        {
            _rb = _rb == null ? GetComponent<Rigidbody>() : _rb;
            _collider = _collider == null ? GetComponent<Collider>() : _collider;
        }

        public void Collect()
        {
            if (IsCollected)
                return;

            IsCollected = true;
            _rb.excludeLayers = _groundLayer;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IBlackHole _))
            {
                Vector3 dir = other.transform.position - (transform.position + _rb.centerOfMass);
                _rb.linearVelocity = dir * 2f;
            }

            /*if (other.TryGetComponent(out BlackHoleCollision _))
            {
                _rb.detectCollisions = false;
            }*/
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            if (!IsCollected)
            {
                if (other.TryGetComponent(out IBlackHole _))
                {
                    _rb.excludeLayers = _groundLayer;
                    Vector3 max = new(_collider.bounds.max.x, other.bounds.min.y, _collider.bounds.max.z);
                    Vector3 min = new(_collider.bounds.min.x, other.bounds.min.y, _collider.bounds.min.z);
                    if (other.bounds.Contains(min) && other.bounds.Contains(max))
                    {
                        _rb.linearVelocity += Physics.gravity;
                    }
                }
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (!IsCollected)
            {
                if (other.TryGetComponent(out IBlackHole _))
                {
                    _rb.excludeLayers = LayerMask.GetMask();
                }
            }
        }
    }
}
