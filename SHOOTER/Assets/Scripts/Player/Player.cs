using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Healthbar _healthBar;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speed;
    private Inventory _inventory;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inventory = FindObjectOfType<Inventory>();
        _health = _maxHealth;
    }

    public void Move(Vector2 moveInput)
    {
        _rigidbody.velocity = moveInput * _speed;
    }

    private void UpdateHPbar()
    {
        _healthBar.UpdateBar(_health, _maxHealth);
    }

    public void DealDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
            Die();
        UpdateHPbar();
    }

    private void Die()
    {
        Destroy(gameObject);
        _inventory.SaveCountData();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Pickup pickup))
        {
            Destroy(pickup.gameObject);
            _inventory.AddItem(pickup.Item);
        }
    }
}
