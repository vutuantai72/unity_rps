using UnityEngine;
public class Message
    {
        private readonly EventType messageType;

        public object Data { get; private set; }

        public EventType MessageType => messageType;

        public Message() { }

        public Message(EventType type)
        {
            messageType = type;
        }

        public void UpdateNewData(object data)
        {
            this.Data = data;
        }

        public Message(EventType type, object data)
        {
            messageType = type;
            this.Data = data;
        }
    }
