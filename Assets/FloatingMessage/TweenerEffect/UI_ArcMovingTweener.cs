//Copyright(c) Luchunpen, 2023.

using System;
using UnityEngine;
using DG.Tweening;

namespace FloatingMessage
{
    public class UI_ArcMovingTweener : TweenerEffect
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

        [SerializeField] private Ease _xEase = Ease.InExpo;
        [SerializeField] private float _xMoveDelay;
        [SerializeField] private Ease _yEase = Ease.InExpo;
        [SerializeField] private float _yMoveDelay;

        private RectTransform _target;
        private Vector2 _targetPos;

        private Tween _tweenX;
        private Tween _tweenY;

        private void OnDestroy()
        {
            ResetState();
        }

        public void Play()
        {
            if (_target == null)
            {
                RaiseOnComplete(null);
                return;
            }

            Play(_target);
        }

        public override void Play(Transform target)
        {
            ResetState();

            _target = target.GetComponent<RectTransform>();
            _targetPos = _target.position;

            float xDuration = _duration - _xMoveDelay;
            float yDuration = _duration - _yMoveDelay;

            _tweenX = RectTrans.DOMoveX(_targetPos.x, xDuration).SetEase(_xEase).SetDelay(_xMoveDelay);
            _tweenY = RectTrans.DOMoveY(_targetPos.y, yDuration).SetEase(_yEase).SetDelay(_yMoveDelay)
                .OnComplete(()=> RaiseOnComplete(null));
        }

        public override void ResetState()
        {
            _target = null;
            if (_tweenX != null)
            {
                _tweenX.Kill();
                _tweenX = null;
            }
            if (_tweenY != null)
            {
                _tweenY.Kill();
                _tweenY = null;
            }
        }
    }
}
