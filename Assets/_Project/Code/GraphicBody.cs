using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Code
{
    public class GraphicBody : MonoBehaviour
    {
        [FormerlySerializedAs("Intensity")] public float intensity;
        [FormerlySerializedAs("Duration")] public float duration;

        private Tweener _scaleTween;
        
        public void StartPingPong(float speed)
        {
            Reset();
            var duration = (0.1f / speed) * 3.6f;
            _scaleTween =
                transform.DOScale(new Vector3(1 + intensity, 1 + intensity, 1 + intensity), duration)
                    .SetLoops(-1, LoopType.Yoyo);
        }

        public void StopPingPong()
        {
            Reset();
        }

        private void Reset()
        {
            if (_scaleTween != null)
            {
                _scaleTween.Complete();
                _scaleTween.Kill();
            }
            transform.localScale = Vector3.one;
        }
    }
}