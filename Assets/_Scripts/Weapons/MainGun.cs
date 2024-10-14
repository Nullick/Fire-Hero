using UnityEngine;

public class MainGun : Weapon
{
    [SerializeField] private Transform _mainPosition;

    [SerializeField] private Transform _leftLevel2Position;
    [SerializeField] private Transform _rightLevel2Position;

    [SerializeField] private Transform _leftLevel3Position;
    [SerializeField] private Transform _rightLevel3Position;

    private Vector2 _direction;

    public void Initialize()
    {
        Level = 1;
        MaxLevel = 3;

        StartTimeFire = .5f;
        TimeBetweenFire = StartTimeFire;

        ProjectileSpeed = 5f;
        StartProjectileSpeed = ProjectileSpeed;

        Damage = 1f;
        StartDamage = Damage;

        WeaponType = WeaponType.MainGun;

        _direction = Vector2.up;
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused)
            return;

        Fire();
    }

    public override void Fire()
    {
        if (TimeBetweenFire <= 0)
        {
            switch (Level)
            {
                case 1:
                    CreateProjectile(_mainPosition, _direction);
                    break;
                case 2:
                    CreateProjectile(_leftLevel2Position, _direction);
                    CreateProjectile(_rightLevel2Position, _direction);
                    break;
                default:
                case 3:
                    CreateProjectile(_leftLevel3Position, _direction);
                    CreateProjectile(_rightLevel3Position, _direction);
                    CreateProjectile(_mainPosition, _direction);
                    break;
            }

            TimeBetweenFire = StartTimeFire;
        }
        else
        {
            TimeBetweenFire -= Time.deltaTime;
        }
    }
}
