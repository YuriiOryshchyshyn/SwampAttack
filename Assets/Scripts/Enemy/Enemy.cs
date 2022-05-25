using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;
    [SerializeField] private float _DamageVisualizationDelay;

    private Player _player;
    private SpriteRenderer _renderer;
    private int _enemyIndex;

    public event UnityAction<Enemy> Dying;

    public Player Target => _player;
    public int Reward => _reward;
    public int Index => _enemyIndex;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        StartCoroutine(DamageVisualizationCoroutine());

        if (_health <= 0)
        {
            Die();
        }
    }

    private IEnumerator DamageVisualizationCoroutine()
    {
        for (float i = 0; i < 1; i += _DamageVisualizationDelay)
        {
            _renderer.color = Color.Lerp(Color.red, Color.white, i);
            yield return null;
        }
    }

    private void Die()
    {
        Dying?.Invoke(this);
        Destroy(gameObject);
    }

    internal void Init(Player player, int index)
    {
        _player = player;
        _enemyIndex = index;
    }
}
