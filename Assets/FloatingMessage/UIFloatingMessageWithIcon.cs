using System;
using UnityEngine;
using UnityEngine.UI;

namespace FloatingMessage
{
    public class UIFloatingMessageWithIcon : UIFloatingMessage
    {
        [SerializeField] protected Image _icon;

        public override void SetMessage(MessageData mData)
        {
            MessageDataWithSprite mds = mData as MessageDataWithSprite;
            if (mds != null && _icon != null)
            {
                _icon.gameObject.SetActive(mds.Icon != null);
                _icon.sprite = mds.Icon;
            }

            base.SetMessage(mData);
            LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
        }

        public override void ItemClear()
        {
            if (_icon != null) 
            {
                _icon.sprite = null;
                _icon.gameObject.SetActive(false);
            }

            base.ItemClear();
        }
    }
}
