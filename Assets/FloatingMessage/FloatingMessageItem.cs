//Copyright(c) Luchunpen, 2023.

using System;
using UnityEngine;

namespace FloatingMessage
{
    public abstract class FloatingMessageItem : MonoBehaviour
    {
        public EventHandler OnComplete;
        public abstract MessageData MData { get; }
        public abstract void SetMessage(MessageData data);
        public abstract void PlayEffect(RectTransform target);
        public abstract void ItemClear();
        protected void OnCompleteTrigger()
        {
            OnComplete?.Invoke(this, null);
        }
    }
}
