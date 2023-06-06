using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerHealthController playerHealthController;
    [SerializeField] private Transform _playerPoint;

    // private void Start()
    // {
    //     var eventDataRequest = new GetPlayerEvent(_playerPoint, _playerHealth);
    //     EventStream.Game.Publish(eventDataRequest);
    // }
}