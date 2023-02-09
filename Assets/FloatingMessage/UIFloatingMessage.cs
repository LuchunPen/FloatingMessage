using System;
using UnityEngine;
using TMPro;
using Nano3.Unity;

namespace FloatingMessage
{
    [RequireComponent(typeof(RectTransform))]
    public class UIFloatingMessage : FloatingMessageItem, IPoolableComponent
    {
        protected MessageData _mData;
        public override MessageData MData { get { return _mData; } }

        [SerializeField] protected TextMeshProUGUI _text;

        [SerializeField] protected TweenerEffect _effect;
        [SerializeField] public Color TextColor
        {
            get
            {
                if (_text == null) { return Color.white; }
                return _text.color;
            }
            set
            {
                if (_text == null) { return; }
                _text.color = value;
            }
        }

        protected void Awake()
        {
            if (_effect != null) {
                _effect.OnComplete += RaiseOnComplete;
            }
        }

        protected virtual void OnDestroy()
        {
            ItemClear();
            if (_effect != null) { _effect.OnComplete -= RaiseOnComplete; }
        }

        public override void SetMessage(MessageData mData)
        {
            if (mData == null) { return; }

            _mData = mData;
            _text.text = mData.Text;
        }

        public override void PlayEffect(RectTransform target = null)
        {
            _effect.Play(target);
        }

        protected void RaiseOnComplete(object sender, EventArgs e)
        {
            OnComplete?.Invoke(this, null);
        }

        public virtual void ItemInitialize() { }

        public override void ItemClear()
        {
            _text.text = "";
            _mData = null;
            
            if (_effect != null) { _effect.ResetState(); }
        }
    }
}
