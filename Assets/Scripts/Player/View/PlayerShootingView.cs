using System;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

namespace Player.View
{
    public class PlayerShootingView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _gunParticles;
        [SerializeField] private Light _gunLight;
        [SerializeField] private AudioSource _gunAudio;
        [SerializeField] private LineRenderer _gunLine;

        private CompositeDisposable _subscription;

        private void Start()
        {
            _subscription = new CompositeDisposable()
            {
                EventStream.Game.Subscribe<GetShootEvent>(OnShoot),
                EventStream.Game.Subscribe<GetDisableEffectsEvent>(DisableEffects),
                EventStream.Game.Subscribe<GetNewDirectionEvent>(SetPosition),
                EventStream.Game.Subscribe<GetAppearanceEffectEvent>(AppearanceEffect)
            };
        }

        private void OnShoot(GetShootEvent data)
        {
            _gunAudio.Play();

            _gunLight.enabled = true;

            _gunParticles.Stop();
            _gunParticles.Play();
        }

        private void DisableEffects(GetDisableEffectsEvent data)
        {
            _gunLine.enabled = false;
            _gunLight.enabled = false;
        }

        private void SetPosition(GetNewDirectionEvent data)
        {
            _gunLine.SetPosition(data.Index, data.Direction);
        }

        private void AppearanceEffect(GetAppearanceEffectEvent data)
        {
            _gunLine.enabled = true;
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}