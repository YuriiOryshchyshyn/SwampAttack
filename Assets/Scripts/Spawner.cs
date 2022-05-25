using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private float _timeAfterLastSpawn;
    private int _currentWaveIndex = 0;
    private int _spawned;
    private bool _isLastWave;
    private int _enemyCounts;

    public event UnityAction AllEnemySpawned;
    public event UnityAction LastEnemyDied;
    public event UnityAction<int, int> EnemyCountChanged;

    private void Start()
    {
        SetWave(_waves[_currentWaveIndex]);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            _spawned++;
            SpawnEnemy();
            _timeAfterLastSpawn = 0;
            EnemyCountChanged?.Invoke(_spawned, _currentWave.Count);
        }

        if (_currentWave.Count <= _spawned)
        {
            if (_waves.Count > _currentWaveIndex + 1)
                AllEnemySpawned?.Invoke();
            else
                _isLastWave = true;

            _enemyCounts = _currentWave.Count;
            _currentWave = null;
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint);
        enemy.Init(_player, _spawned);
        enemy.Dying += OnEnemyDying;
    }

    public void NextWave()
    {
        SetWave(_waves[++_currentWaveIndex]);
        _spawned = 0;
    }

    private void SetWave(Wave wave)
    {
        _currentWave = wave;
        EnemyCountChanged?.Invoke(0, 1);
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        if (_isLastWave && enemy.Index == _enemyCounts)
            LastEnemyDied?.Invoke();
        _player.AddMoney(enemy.Reward);
    }
}

[System.Serializable]
public class Wave
{
    public Enemy Template;
    public float Delay;
    public int Count;
}