using System;
using UnityEngine;

namespace FloatingMessage
{
    public abstract class  TweenerEffect : MonoBehaviour
    {
        public event EventHandler OnComplete;
        public abstract float Duration { get; set; }
        public abstract void Play(Transform target);
        public abstract void ResetState();

        public virtual void RaiseOnComplete(EventArgs e)
        {
            OnComplete?.Invoke(this, e);
        }
    }
}
