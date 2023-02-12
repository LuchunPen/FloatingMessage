//Copyright(c) Luchunpen, 2023.

using System;
using UnityEngine;

namespace FloatingMessage
{
    public class MessageDataWithSprite : MessageData
    {
        public virtual Sprite Icon { get; set; }
        public MessageDataWithSprite(long id) : base(id) { }
    }
}