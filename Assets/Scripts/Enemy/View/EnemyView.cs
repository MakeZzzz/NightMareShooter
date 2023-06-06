using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource enemyAudio;
    [SerializeField] private ParticleSystem hitParticles;

    private CompositeDisposable _subscription;

    private void Start()
    {
        _subscription = new CompositeDisposable()
        {
            EventStream.Game.Subscribe<GetEnemyAudioEvent>(PlayEnemyAudio),
            EventStream.Game.Subscribe<GetPlayHitEvent>(PlayHitParticles),
            EventStream.Game.Subscribe<GetEnemyAudioDeadEvent>(PlayDeathAudio),
            EventStream.Game.Subscribe<GetPlayerDeadEvent>(PlayerDead)
        };
    }

    private void PlayEnemyAudio(GetEnemyAudioEvent data)
    {
        enemyAudio.Play();
    }

    private void PlayHitParticles(GetPlayHitEvent hitPoint)
    {
        hitParticles.transform.position = hitPoint.Point;
        hitParticles.Play();
    }

    public void EnemyDead( )
    {
        _anim.SetTrigger("Dead");
    }

    private void PlayDeathAudio(GetEnemyAudioDeadEvent data)
    {
        enemyAudio.clip = _deathClip;
        enemyAudio.Play();
    }

    private void PlayerDead(GetPlayerDeadEvent data)
    {
        _anim.SetTrigger("PlayerDead");
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}