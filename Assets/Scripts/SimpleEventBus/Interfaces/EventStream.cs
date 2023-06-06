using System.Collections;
using System.Collections.Generic;
using SimpleEventBus;
using SimpleEventBus.Interfaces;
using UnityEngine;

public static class EventStream
{
   public static IEventBus Game { get; } = new EventBus();
}
