using SimpleEventBus.Events;
using UnityEngine;

namespace Events
{
    public class GetPlayerEvent : EventBase
    {
        public Transform PlayerPoint { get; } 
        public PlayerHealthController PlayerHealthController { get; }

        public GetPlayerEvent(Transform playerPoint, PlayerHealthController playerHealthController)
        {
            PlayerPoint = playerPoint;
            PlayerHealthController = playerHealthController;
        }
    }
}