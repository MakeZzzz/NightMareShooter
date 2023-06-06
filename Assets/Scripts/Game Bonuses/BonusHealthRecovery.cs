using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events;
public class BonusHealthRecovery : MonoBehaviour
{
    private int _rotationSpeed = 100;
    private void Update()
    {
        transform.Rotate(0,0 , _rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision player)
    {
        
        if (player.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
