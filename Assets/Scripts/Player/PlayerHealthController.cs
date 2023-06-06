using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Events;
using JetBrains.Annotations;
using SimpleEventBus.Disposables;
using UnityEngine.Events;

public class PlayerHealthController : MonoBehaviour
{
    public UnityEvent HeartDestroyed;
    public int CurrentHealth => _currentHealth;
    
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShootingController playerShootingController;
    [SerializeField] private int _startingHealth = 100;
    [SerializeField] private int _currentHealth;

    private bool isDead;

    private void Awake()
    {
        _particleSystem.Stop();
        _currentHealth = _startingHealth;
    }

    public void TakeDamage(int amount)
    {
        var eventDataRequest = new GetDamageEvent();
        EventStream.Game.Publish(eventDataRequest);

        _currentHealth -= amount;

        var eventDataRequest2 = new GetCurrentHealthEvent(_currentHealth);
        EventStream.Game.Publish(eventDataRequest2);

        var eventDataRequest3 = new GetEnemyTakeDamageEvent();
        EventStream.Game.Publish(eventDataRequest3);

        if (_currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        var eventDataRequest = new GetDisableEffectsEvent();
        EventStream.Game.Publish(eventDataRequest);

        var eventDataRequest2 = new GetPlayerDeadEvent();
        EventStream.Game.Publish(eventDataRequest2);

        _playerMovement.enabled = false;
        playerShootingController.enabled = false;
    }

    private void OnCollisionEnter(Collision heart)
    {
        if (heart.collider.CompareTag("Heart"))
        {
            _particleSystem.Play();
            _particleSystem.transform.localPosition = Vector3.zero;
            StartCoroutine(DisableParticleSystem());
            TakeHealthBonus(20);
            HeartDestroyed.Invoke();
            if (_currentHealth > 100)
            {
                _currentHealth = 100;
            }
        }
    }

    private void TakeHealthBonus(int amount)
    {
        _currentHealth += amount;
        var eventDataRequest = new GetCurrentHealthEvent(_currentHealth);
        EventStream.Game.Publish(eventDataRequest);
    }

    [UsedImplicitly]
    IEnumerator DisableParticleSystem()
    {
        yield return new WaitForSeconds(1f);
        _particleSystem.Stop();
    }
    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}