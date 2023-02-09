using System;
using UnityEngine;

namespace FloatingMessage.Example
{
    public class FM_Example : MonoBehaviour
    {
        [SerializeField] private UIFloatingInformer _informer;
        [SerializeField] private Transform _worldObject;

        public void CreateMessage()
        {
            ScoreMessageData sm = new ScoreMessageData(0, UnityEngine.Random.Range(10, 50));
            _informer.CreateMessage(sm, _worldObject.position);
        }
    }
}
