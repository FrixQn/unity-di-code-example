using UnityEngine;
using CandyCoded.HapticFeedback;
using VContainer;
using DemoProject.Effects;
using DemoProject.Core;
using System.Collections.Generic;

namespace DemoProject.Gameplay
{
    [RequireComponent(typeof(SphereCollider))]
    public class BlackHole : MonoBehaviour, IBlackHole
    {
        private const int SCORE_TO_SIZE_UP = 10;
        private const float DEFAULT_SIZE = 1.5f;
        private readonly Vector3 _defaultScale = new(DEFAULT_SIZE, 1f, DEFAULT_SIZE);

        [SerializeField] private BlackHoleCollision _collision;
        [SerializeField] private SizeUpEffect _sizeUpEffect;
        private ISoundsController _soundsController;
        private IInputManager _inputManager;
        private SphereCollider _sphereCol;
        private float _moveSpeed;
        private float _shootForce;
        private int _score;
        private int _stage;
        private Vector2 _moveDir;
        private Bounds _movementBounds;
        private Stack<Bullet> _bullets = new();

        public Vector3 Position => transform.position;

        public event SizeUppedDelegate SizeUpped;
        public event StageProgressChanged StageProgressChanged;
        public event ObjectFalledDelegate ObjectFalled;

        [Inject]
        private void Inject(ISoundsController soundsController, IInputManager inputManager)
        {
            _soundsController = soundsController;
            _inputManager = inputManager;
        }

        private void Awake()
        {
            _sphereCol = GetComponent<SphereCollider>();
            _collision.Collided += OnCollide;
        }

        public void Initialize(float movementSpeed, float shootingForce, Bounds bounds)
        {
            _moveSpeed = movementSpeed;
            _shootForce = shootingForce;
            SetMovementBounds(bounds);
        }

        public void Shoot()
        {
            if (_bullets.TryPop(out Bullet bullet))
            {
                bullet.SetActive(true);
                bullet.transform.SetParent(null);
                bullet.transform.SetPositionAndRotation(transform.position + Vector3.right * Random.Range(-0.5f, 0.5f), Quaternion.identity);
                bullet.Move(transform.forward, _shootForce);
            }
        }

        private void SetMovementBounds(Bounds bounds)
        {
            _movementBounds = bounds;
        }

        private void OnCollide(Collider obj)
        {
            if (obj.TryGetComponent(out ICollectableObject collectable))
            {
                if (collectable.IsCollected)
                    return;

                collectable.Collect();
                collectable.GameObject.SetActive(false);
                ObjectFalled?.Invoke(collectable.Score);
                HapticFeedback.HeavyFeedback();
                _soundsController.PlaySound("Take");
                
                if (collectable is Bullet bullet) 
                    _bullets.Push(bullet);

                IncreaseScore(collectable.Score);
                IncreaseSize();
            }
        }

        private void IncreaseScore(int increment)
        {
            increment = Mathf.Clamp(increment, 0, int.MaxValue);

            if (_score + increment < 0)
                _score = int.MaxValue;
            else
                _score += Mathf.Clamp(increment, 0, int.MaxValue);

            StageProgressChanged?.Invoke(increment, _stage, Mathf.InverseLerp(0, SCORE_TO_SIZE_UP, _score));
        }

        private void IncreaseSize()
        {
            if (_score < SCORE_TO_SIZE_UP)
                return;

            if (_score >= SCORE_TO_SIZE_UP)
            {
                _score -= SCORE_TO_SIZE_UP;
                _stage++;
                _soundsController.PlaySound("SizeUp");
                SizeUpped?.Invoke(_stage);
            }

            transform.localScale = CalculateScale();
            _moveSpeed += _stage;
        }

        private Vector3 CalculateScale()
        {
            Vector3 scale = _defaultScale * _stage;
            scale.y = _defaultScale.y;
            return scale;
        }

        private float CalculateRelativePos(float startPos, bool shiftLeft, float moveShift = 0f)
        {
            return startPos + (shiftLeft ? -1f : +1f) * GetRelativeSphereRadius() + _moveSpeed * Time.deltaTime * moveShift;
        }

        private float GetRelativeSphereRadius()
        {
            return _sphereCol.radius * (transform.localScale.x + transform.localScale.z) / 2f;
        }

        private bool IsInBounds()
        {
            float radius = GetRelativeSphereRadius();
            Vector3 range = new (radius, 0f, radius);
            Vector3 pos = transform.position;
            pos.y = _movementBounds.center.y;

            return _movementBounds.Contains(pos + range) && _movementBounds.Contains(pos - range);
        }

        private Vector3 CalculatePositionInBounds()
        {
            float radius = GetRelativeSphereRadius();
            float minX = CalculateRelativePos(transform.position.x, true);
            float minZ = CalculateRelativePos(transform.position.z, true);
            float maxX = CalculateRelativePos(transform.position.x, false);
            float maxZ = CalculateRelativePos(transform.position.z, false);

            float clampedX = transform.position.x;
            float clampedZ = transform.position.z;

            if (minX < _movementBounds.min.x)
            {
                clampedX = _movementBounds.min.x + radius;
            }
            else if (maxX > _movementBounds.max.x)
            {
                clampedX = _movementBounds.max.x - radius;
            }

            if (minZ < _movementBounds.min.z)
            {
                clampedZ = _movementBounds.min.z + radius;
            }
            else if (maxZ > _movementBounds.max.z)
            {
                clampedZ = _movementBounds.max.z - radius;
            }

            if (clampedX == transform.position.x && clampedZ == transform.position.z)
            {
                return transform.position;
            }

            return new Vector3(clampedX, transform.position.y, clampedZ);
        }

        private void Update()
        {
            _moveDir = _inputManager.GetInput().Joystick;
            if (IsInBounds())
            {
                transform.Translate(_moveSpeed * Time.deltaTime * new Vector3(_moveDir.x, 0f, _moveDir.y));
            }
            else
            {
                transform.position = CalculatePositionInBounds();
            }
        }
    }
}
