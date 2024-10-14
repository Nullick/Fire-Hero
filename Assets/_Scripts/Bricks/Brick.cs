using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
[RequireComponent(typeof(HealthCount))]
public class Brick : MonoBehaviour, IDamagable
{
    [SerializeField] protected float _currentHealth;
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _speed = 1f;
    [SerializeField] protected HealthCount _healthCount;
    [SerializeField] protected Gradient _gradient;

    private Health _health;
    private SpriteRenderer _spriteRenderer;
    private BoundsCheck _bnd;
    private float _maxHealthPossible;

    public BoundsCheck Bnd => _bnd;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public virtual void Initialize(Health health, float maxHealthPossible)
    {
        _health = health;
        _health.Died += OnDied;

        _currentHealth = _health.Value;
        _maxHealth = _currentHealth;

        _bnd = GetComponent<BoundsCheck>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _healthCount = GetComponent<HealthCount>();

        _healthCount.Initialize(_health);

        _maxHealthPossible = maxHealthPossible;

        ChangeColor();
    }

    private void OnDied()
    {
        Destroy(gameObject);
        EnemyManager.Instance.RemoveList(this);
    }

    public void TakeDamage(float damageValue)
    {
        _health.Reduce(damageValue);
        _currentHealth = _health.Value;
        ChangeColor();
    }

    private void OnDestroy()
    {
        _health.Died -= OnDied;

        if (!_bnd.OffDown)
            EnemyManager.Instance.OnEnemyDied();
    }

    public virtual void Move()
    {
        Vector2 tempPos = transform.position;
        tempPos.y -= _speed * Time.deltaTime;
        transform.position = tempPos;
    }

    private void ChangeColor()
    {
        _spriteRenderer.color = _gradient.Evaluate(_currentHealth / _maxHealthPossible);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile proj = collision.gameObject.GetComponent<Projectile>();

        if (proj != null)
        {
            TakeDamage(proj.Damage);
        }
    }
}
