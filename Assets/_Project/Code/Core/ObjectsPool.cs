using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace DemoProject.Core
{
    public class ObjectsPool : MonoBehaviour, IObjectsPool, IInitializable
    {
        private readonly Dictionary<GameObject, Queue<GameObject>> _pool = new ();
        private readonly List<GameObject> _nonReferenced = new();

        public void Initialize() { }

        public void CreateInstances(GameObject obj, int count)
        {
            if (!_pool.ContainsKey(obj))
            {
                Queue<GameObject> queue = new();
                for (int i = 0; i < count; i++)
                {
                    CreateInstance(obj, out GameObject instance, false);
                    queue.Enqueue(instance);
                }

                _pool.Add(obj, queue);
            }
            else
            {
                int remainingInstances = Mathf.Max(count, _pool[obj].Count);
                if (remainingInstances > _pool[obj].Count)
                {
                    for (int i = 0; i < count - _pool[obj].Count; i++)
                    {
                        CreateInstance(obj, out GameObject instance, false);
                        _pool[obj].Enqueue(instance);
                    }
                }
            }
        }

        public void Add(GameObject gameObject)
        {
            AddObjectToPool(gameObject);
        }

        public void Add(GameObject refference, GameObject instance)
        {
            AddObjectToPool(instance, refference);
        }

        private void AddObjectToPool(GameObject instance, GameObject refference = null)
        {
            if (instance == null)
                return;
            
            if (refference == null)
            {
                if (_nonReferenced.Contains(instance))
                    return;

                _nonReferenced.Add(instance);
            }
            else
            {
                if (_pool.TryGetValue(refference, out Queue<GameObject> queue))
                {
                    queue.Enqueue(instance);
                }
                else
                {
                    queue = new Queue<GameObject>();
                    queue.Enqueue(instance);
                    _pool.Add(refference, queue);
                }
            }

            instance.SetActive(false);
            instance.transform.SetParent(transform);
            instance.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public void BackToPool(GameObject refference, GameObject gameObject)
        {
            AddObjectToPool(gameObject, refference);
        }

        public void Clear()
        {
            _pool.Clear();
        }

        public GameObject Get(GameObject refference)
        {
            GameObject instance;
            if (_pool.TryGetValue(refference, out Queue<GameObject> queue))
            {
                if (queue.Count == 0)
                {
                    CreateInstance(refference, out instance);
                    AddObjectToPool(instance);
                }
                else
                {
                    instance = queue.Dequeue();
                }
            }
            else
            {
                CreateInstance(refference, out instance);
            }

            instance.transform.SetParent(null);
            instance.SetActive(true);
            return instance;
        }

        public T Get<T>(T refference) where T : Component
        {
            if (Get(refference.gameObject).TryGetComponent(out T component))
            {
                return component;
            }
            
            throw new MissingComponentException($"GameObject {refference.gameObject} doesn't exist component {typeof(T)}");
        }

        private void CreateInstance(GameObject refference, out GameObject obj, bool keepActive = true)
        {
            obj = Instantiate(refference, transform);
            obj.SetActive(keepActive);
        }

        public void Release(GameObject refference)
        {
            if (_pool.TryGetValue(refference, out var queue))
            {
                Destroy(queue.Dequeue());
            }
        }
    }
}
