using System;
using UnityEngine;
using System.Collections;
using Events;
using SimpleEventBus.Disposables;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyHealthController _enemyHealthController;
    [SerializeField] private UnityEngine.AI.NavMeshAgent _nav;
   
    
    private Transform _playerPoint;
    private PlayerHealthController _playerHealthController;
    private CompositeDisposable _subscription;
    

    public void Initialize(PlayerHealthController playerHealthController)
    {
        _playerHealthController = playerHealthController;
        _playerPoint = _playerHealthController.transform;
    }
    
    void Update()
    {
        if (_enemyHealthController.CurrentHealth > 0 && _playerHealthController.CurrentHealth > 0)
        {
            _nav.SetDestination(_playerPoint.position);
        }
        else
        {
            _nav.enabled = false;
        }
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}