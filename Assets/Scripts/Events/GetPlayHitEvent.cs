using SimpleEventBus.Events;
using UnityEngine;

namespace Events
{
    public class GetPlayHitEvent : EventBase
    {
        public Vector3 Point { get; }
        
        public GetPlayHitEvent(Vector3 point)
        {
            Point = point;
        }
    }
}