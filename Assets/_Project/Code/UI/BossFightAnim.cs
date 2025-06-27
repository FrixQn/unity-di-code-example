using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DemoProject.UI
{
    public class BossFightAnim : MonoBehaviour
    {
        [SerializeField] private float _animationDuration;
        [SerializeField] private Image _top;
        [SerializeField] private Image _bottom;
        [SerializeField] private TextMeshProUGUI _text;

        public async Task Play()
        {
            float halfDuration = _animationDuration / 2f;
            var sequence = DOTween.Sequence().
                Insert(0f, _top.rectTransform.DOSizeDelta(new Vector2(_top.rectTransform.sizeDelta.x, 1000f), halfDuration)).
                Insert(0f, _bottom.rectTransform.DOSizeDelta(new Vector2(_bottom.rectTransform.sizeDelta.x, 1000f), halfDuration)).
                Insert(0f, _text.DOFade(1f, halfDuration)).SetLoops(2, LoopType.Yoyo).AppendInterval(halfDuration);

            await sequence.AsyncWaitForCompletion();
        }

    }
}
