using System;
using Events;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthController : MonoBehaviour
{
    public int CurrentHealth => currentHealth;
    public UnityEvent EnemyDieEvent;
    
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private float sinkSpeed = 2.5f;
    [SerializeField] private int scoreValue = 10;

    [SerializeField] private CapsuleCollider capsuleCollider;

    private bool isDead;
    private bool isSinking;


    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        var eventDataRequest = new GetEnemyAudioEvent();
        EventStream.Game.Publish(eventDataRequest);

        currentHealth -= amount;

        var eventDataRequest2 = new GetPlayHitEvent(hitPoint);
        EventStream.Game.Publish(eventDataRequest2);

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;
        
        EnemyDieEvent.Invoke();
        var eventDataRequest2 = new GetEnemyAudioDeadEvent();
        EventStream.Game.Publish(eventDataRequest2);
    }


    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}