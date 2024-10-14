using UnityEngine;

public class SideGun : Weapon
{
    [SerializeField] private Transform _leftPosition;
    [SerializeField] private Transform _rightPosition;

    private Vector2 _left_45;
    private Vector2 _right_45;

    private Vector2 _left_30;
    private Vector2 _right_30;

    private Vector2 _left_60;
    private Vector2 _right_60;

    public void Initialize()
    {
        Level = 0;
        MaxLevel = 2;

        StartTimeFire = .5f;
        TimeBetweenFire = StartTimeFire;

        ProjectileSpeed = 5f;
        StartProjectileSpeed = ProjectileSpeed;

        Damage = 1f;
        StartDamage = Damage;

        WeaponType = WeaponType.SideGun;

        InitializeDirections();
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused || Level == 0)
            return;

        Fire();
    }

    private void InitializeDirections()
    {
        _left_45 = new Vector2(-1f, 1f);
        _right_45 = new Vector2(1f, 1f);

        _left_30 = new Vector2(-Mathf.Pow(3f, .5f)/3, 1f);
        _right_30 = new Vector2(Mathf.Pow(3f, .5f)/3, 1f);

        _left_60 = new Vector2(-Mathf.Pow(3f, .5f), 1f);
        _right_60 = new Vector2(Mathf.Pow(3f, .5f), 1f);
    }

    public override void Fire()
    {
        if (TimeBetweenFire <= 0)
        {
            switch (Level)
            {
                case 1:
                    CreateProjectile(_leftPosition, _left_45);
                    CreateProjectile(_rightPosition, _right_45);
                    break;
                case 2:
                    CreateProjectile(_leftPosition, _left_30);
                    CreateProjectile(_rightPosition, _right_30);
                    CreateProjectile(_leftPosition, _left_60);
                    CreateProjectile(_rightPosition, _right_60);
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
