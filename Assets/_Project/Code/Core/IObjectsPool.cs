using UnityEngine;

namespace DemoProject.Core
{
    public interface IObjectsPool
    {
        public void Initialize();

        public void CreateInstances(GameObject refference, int count);
        public void Add(GameObject instance);
        public void Add(GameObject refference, GameObject instance);
        public GameObject Get(GameObject refference);
        public T Get<T>(T refference) where T : Component;
        public void BackToPool(GameObject refference, GameObject gameObject);
    }
}
