using SimpleEventBus.Events;
using UnityEngine;

namespace Events
{
    public class GetNewDirectionEvent : EventBase
    {
        public int Index { get; }
        public Vector3 Direction { get; }

        public GetNewDirectionEvent(int index, Vector3 direction)
        {
            Index = index;
            Direction = direction;
        }
    }
}