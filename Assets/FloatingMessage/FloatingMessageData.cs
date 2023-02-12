//Copyright(c) Luchunpen, 2023.

using System;

namespace FloatingMessage
{
    public class MessageData
    {
        protected long _id;
        public long ID { get { return _id; } }
        public virtual string Text { get; set; }
        public MessageData(long id)
        {
            _id = id;
        }
    }

    public class ScoreMessageData : MessageData
    {
        private int _scorePoints;
        public int ScorePoints { get { return _scorePoints; } }

        public override string Text 
        {
            get
            {
                if (_scorePoints > 0) { return "+" + _scorePoints; }
                else if (_scorePoints < 0) { return "-" + _scorePoints; }
                else return "0";
            }
            set { return; }
        }

        public ScoreMessageData(long id, int score)
            : base(id)
        {
            _scorePoints = score;
        }
    }
}
