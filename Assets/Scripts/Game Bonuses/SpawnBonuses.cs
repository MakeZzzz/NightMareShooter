using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnBonuses : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private int _heartsCount;

    private int _heartsLimit = 2;

    private void Start()
    {
        InvokeRepeating("SpawnHeart", 5f, 3f);
    }

    private void SpawnHeart()
    {
        if (_heartsCount < _heartsLimit)
        {
            int spawnIndex = Random.Range(0, _spawnPoints.Length);
            Transform spawnPoint = _spawnPoints[spawnIndex];

            GameObject Heart = Instantiate(_heartPrefab, spawnPoint.transform.position, spawnPoint.rotation);
            _heartsCount++;
        }
    }

    [UsedImplicitly]
    public void RemoveHeart()
    {
        _heartsCount--;
    }
}