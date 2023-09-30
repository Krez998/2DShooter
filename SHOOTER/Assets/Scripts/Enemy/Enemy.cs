using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private Healthbar _healthBar;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    private Rigidbody2D _rb;
    private bool _isAttack;
    private LootSpawner _lootSpawner;
    private WaitForSeconds _attackDelay = new WaitForSeconds(0.5f);
    private Transform _target;
    private Coroutine _attackRoutine;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lootSpawner = FindObjectOfType<LootSpawner>();
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            if (Vector2.Distance(transform.position, _target.position) <= _attackRange && !_isAttack)
            {
                _rb.velocity = Vector2.zero;
                _attackRoutine = StartCoroutine(AttackPlayerRoutine());
            }
            else if (!_isAttack)
            {
                Vector2 _chaseDirection = (_target.transform.position - transform.position).normalized;
                _rb.velocity = _chaseDirection * _speed;
            }
        }
        else
        {
            if (_attackRoutine != null) StopCoroutine(_attackRoutine);
            _rb.velocity = Vector2.zero;
        }
    }

    private void DealDamage()
    {
        if (_target.TryGetComponent(out Player player))
        {
            player.DealDamage(_damage);
        }
    }

    private IEnumerator AttackPlayerRoutine()
    {
        _isAttack = true;
        while (true)
        {
            if (Vector2.Distance(transform.position, _target.position) > _attackRange)
            {
                _isAttack = false;
                yield break;
            }
            yield return _attackDelay;
            DealDamage();
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void DealDamage(float damage)
    {
        _health -= damage;        
        if (_health <= 0)
            Die();
        UpdateHPbar();
    }

    private void UpdateHPbar()
    {
        _healthBar.UpdateBar(_health, _maxHealth);
    }

    private void Die()
    {
        _lootSpawner.SpawnLoot(transform);
        EventBus.EnemyEliminated?.Invoke();
        Destroy(gameObject);
    }
}
