using System;
using UnityEngine;
using DG.Tweening;

namespace FloatingMessage
{
    public class UI_FloatingTweener : TweenerEffect
    {
        [SerializeField] protected RectTransform _rt;
        public RectTransform RectTrans
        {
            get
            {
                if (_rt == null) { _rt = this.GetComponent<RectTransform>(); }
                return _rt;
            }
        }

        [SerializeField] private float _duration;
        public override float Duration { get { return _duration; } set { _duration = value; } }

        [SerializeField] private Vector2 _minEndOffset;
        [SerializeField] private Vector2 _maxEndOffset;
        [SerializeField] private Ease _easeType;
        private Tween _tween;

        public override void Play(Transform target = null)
        {
            ResetState();

            float dx = UnityEngine.Random.Range(_minEndOffset.x, _maxEndOffset.x);
            float dy = UnityEngine.Random.Range(_minEndOffset.y, _maxEndOffset.y);

            _tween = RectTrans.DOAnchorPos(RectTrans.anchoredPosition + new Vector2(dx, dy), _duration)
                .SetEase(_easeType)
                .OnComplete(() => RaiseOnComplete(null));
        }
        public override void ResetState()
        {
            if (_tween != null)
            {
                _tween.Kill();
                _tween = null;
            }
        }
    }
}
