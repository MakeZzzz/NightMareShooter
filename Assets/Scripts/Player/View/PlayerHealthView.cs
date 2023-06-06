using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Image _damageImage;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _playerAudio;

    private bool damaged;
    private Color _flashColour = new Color(1f, 0f, 0f, 0.1f);
    private float _flashSpeed = 5f;
    
    private CompositeDisposable _subscription;

    private void Start()
    {
        _subscription = new CompositeDisposable()
        {
            EventStream.Game.Subscribe<GetPlayerDeadEvent>(Death),
            EventStream.Game.Subscribe<GetEnemyTakeDamageEvent>(TakeDamageAudio),
            EventStream.Game.Subscribe<GetCurrentHealthEvent>(TakeHealthBonus),
            EventStream.Game.Subscribe<GetDamageEvent>(GetDamage),
            EventStream.Game.Subscribe<GetPlayerWalkEvent>(PlayerWalking)
        };
    }

    public void Death(GetPlayerDeadEvent data)
    {
        _anim.SetTrigger("Die");

        _playerAudio.clip = _deathClip;
        _playerAudio.Play();
    }

    private void TakeDamageAudio(GetEnemyTakeDamageEvent data)
    {
        _playerAudio.Play();
    }
    private void GetDamage(GetDamageEvent data)
    {
        damaged = true;
    }

    private void TakeHealthBonus(GetCurrentHealthEvent data)
    {
        _healthSlider.value = data.CurrentHealth;
    }

    private void PlayerWalking(GetPlayerWalkEvent data)
    {
        _anim.SetBool("IsWalking", data.IsWalking);
    }
    void Update()
    {
        if (damaged)
        {
            _damageImage.color = _flashColour;
        }
        else
        {
            _damageImage.color = Color.Lerp(_damageImage.color, Color.clear, _flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }
    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}