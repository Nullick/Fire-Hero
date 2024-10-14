using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private BoundsCheck _bnd;

    private float _damage;
    private float _speed;
    private Vector2 _direction;

    private Rigidbody2D _rb;

    public float Damage => _damage;
    public BoundsCheck Bnd => _bnd;

    private void Start()
    {
        _bnd = GetComponent<BoundsCheck>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(float damage, float speed, Vector2 direction)
    {
        _damage = damage;
        _speed = speed;
        _direction = direction.normalized;
    }

    public void Move()
    {
        if (_rb != null)
        {
            _rb.velocity = _direction * _speed;
        }
    }

    public void StopProjectile()
    {
        if(_rb != null)
        {
            _rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Brick") || _bnd.OffUp)
        {
            Destroy(gameObject);
            ProjectileManager.Instance.RemoveList(this);
        }
    }
}