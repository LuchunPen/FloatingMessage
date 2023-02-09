using System;
using System.Collections.Generic;
using UnityEngine;
using Nano3.Unity;

namespace FloatingMessage
{
    public class UIFloatingInformer : ComponentPool<UIFloatingMessage>
    {
        [Flags]
        public enum OffsetScatter
        {
            Constant = 0,
            ScatterX = 1,
            ScatterY = 2
        }
        public event EventHandler<MessageData> OnMessageFloatingComplete;

        [Tooltip("Camera for object on scene")]
        [SerializeField] private Camera _worldCamera;
        [SerializeField] private RectTransform _informerRootPanel;
        [SerializeField] private RectTransform _floatingTarget;
        [SerializeField] private Color _messageColor;
        [SerializeField] private Vector2 _spawnPositionOffset;
        [SerializeField] private OffsetScatter _scatterType;

        private List<UIFloatingMessage> _activeMessages = new List<UIFloatingMessage>();
        public UIFloatingMessage[] ActiveMessages { get { return _activeMessages.ToArray(); } }

        private void Start()
        {
            if (_worldCamera == null) {
                _worldCamera = Camera.main;
            }
        }

        public void CreateMessage(MessageData msg, Vector3 worldPoint)
        {
            if (_worldCamera == null) { return; }

            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPoint);

            CreateMessage(msg, screenPoint);
        }

        public void CreateMessage(MessageData msg, Vector2 screenPoint)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_informerRootPanel, screenPoint, null, out localPoint);

            UIFloatingMessage f_msg = Pop();
            f_msg.transform.SetParent(_informerRootPanel);
            f_msg.transform.localScale = Vector3.one;
            f_msg.transform.localRotation = Quaternion.identity;

            Vector2 offset = new Vector2(0, 0);
            offset.x = (_scatterType & OffsetScatter.ScatterX) != 0 ? UnityEngine.Random.Range(-_spawnPositionOffset.x, _spawnPositionOffset.x) : _spawnPositionOffset.x;
            offset.y = (_scatterType & OffsetScatter.ScatterY) != 0 ? UnityEngine.Random.Range(-_spawnPositionOffset.y, _spawnPositionOffset.y) : _spawnPositionOffset.y;

            f_msg.transform.localPosition = localPoint + offset;

            f_msg.OnComplete += OnMessageCompleteHandler;
            f_msg.TextColor = _messageColor;
            f_msg.SetMessage(msg);
            f_msg.PlayEffect(_floatingTarget);

            _activeMessages.Add(f_msg);
        }

        protected virtual void OnMessageCompleteHandler(object sender, EventArgs e)
        {
            UIFloatingMessage msg = sender as UIFloatingMessage;
            if (msg != null)
            {
                msg.OnComplete -= OnMessageCompleteHandler;
                _activeMessages.Remove(msg);

                OnMessageFloatingComplete?.Invoke(this, msg.MData);
                Release(msg);
            }
        }

        public override void Clear()
        {
            UIFloatingMessage[] am = _activeMessages.ToArray();
            for (int i = 0; i < am.Length; i++)
            {
                OnMessageCompleteHandler(am, null);
            }
            _activeMessages.Clear();

            base.Clear();
        }
    }
}
