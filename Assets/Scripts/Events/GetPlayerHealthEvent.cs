using SimpleEventBus.Events;
using UnityEngine;

namespace Events
{
    public class GetPlayerHealthEvent : EventBase
    {
        public PlayerHealthController PlayerHealthController { get; }

        public GetPlayerHealthEvent(PlayerHealthController playerHealthController)
        {
            PlayerHealthController = playerHealthController;
        }
        
    }
}