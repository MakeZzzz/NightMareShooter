using SimpleEventBus.Events;

namespace Events
{
    public class GetPlayerWalkEvent : EventBase
    {
        public bool IsWalking { get; }

        public GetPlayerWalkEvent(bool isWalking)
        {
            IsWalking = isWalking;
        }
    }
}