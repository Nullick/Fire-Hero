using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private Health _health;

    public Health Health => _health;

    public void Initialize(Health health)
    {
        _health = health;
        _health.Died += OnDied;
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused)
            return;

        Move();
    }

    private void Move()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.y = -3.5f;
        transform.position = worldPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("Brick"))
            TakeDamage(1f);
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damageValue)
    {
        _health.Reduce(damageValue);
    }

    private void OnDestroy()
    {
        _health.Died -= OnDied;
    }
}