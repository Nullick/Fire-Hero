using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private List<Projectile> _projectiles = new List<Projectile>();

    public static ProjectileManager Instance;

    public void Initialize()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused)
        {
            foreach(var projectile in _projectiles)
            {
                projectile.StopProjectile();
            }

            return;
        }

        MoveProjectile();
    }

    public void MoveProjectile()
    {
        CheckProjectile();

        foreach (var projectile in _projectiles)
        {
            projectile.Move();
        }
    }

    public void CheckProjectile()
    {
        for (int i = 0; i < _projectiles.Count; i++)
        {
            if (_projectiles[i].Bnd.OffUp)
            {
                Destroy(_projectiles[i].gameObject);
                _projectiles.RemoveAt(i);
            }
        }
    }

    public void AddList(Projectile projectile)
    {
        _projectiles.Add(projectile);
    }

    public void RemoveList(Projectile projectile)
    {
        _projectiles.Remove(projectile);
    }
}
