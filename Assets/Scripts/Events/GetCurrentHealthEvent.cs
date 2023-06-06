using SimpleEventBus.Events;

namespace Events
{
    public class GetCurrentHealthEvent : EventBase
    {
        public int CurrentHealth { get; }

        public GetCurrentHealthEvent(int currentHealth)
        {
            CurrentHealth = currentHealth;
        }
    }
}