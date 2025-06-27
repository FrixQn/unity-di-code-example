using UnityEngine;

namespace DemoProject.UI
{
    public abstract class BaseUIObject : MonoBehaviour
    {
        public virtual void SetVisible(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }

        protected virtual void OnEnable()
        {
            Subscribe();
        }

        protected virtual void Subscribe() { }

        protected virtual void OnDisable()
        {
            Unsubscribe();
        }

        protected virtual void Unsubscribe() { }
    }
}
