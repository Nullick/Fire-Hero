using UnityEngine;

public class Helpers : Weapon
{
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;

    [SerializeField] private SpriteRenderer _leftRenderer;
    [SerializeField] private SpriteRenderer _rightRenderer;

    private Vector2 _direction_up;

    private Vector2 _direction_left_45;
    private Vector2 _direction_right_45;

    private Vector2 _direction_left_30;
    private Vector2 _direction_right_30;

    private Vector2 _direction_left_60;
    private Vector2 _direction_right_60;

    public void Initialize()
    {
        Level = 0;
        MaxLevel = 3;

        StartTimeFire = .2f;
        TimeBetweenFire = StartTimeFire;

        ProjectileSpeed = 10f;
        StartProjectileSpeed = ProjectileSpeed;

        Damage = .2f;
        StartDamage = Damage;

        WeaponType = WeaponType.Helper;

        InitializeDirections();

        UpgradeLevelUpper.MakeVisible += OnMakeVisible;

        //_leftRenderer = GetComponent<SpriteRenderer>();
        //_rightRenderer = GetComponent<SpriteRenderer>();

        if (_leftRenderer != null && _rightRenderer != null)
        {
            _leftRenderer.enabled = false;
            _rightRenderer.enabled = false;
        }

    }

    private void InitializeDirections()
    {
        _direction_up = Vector2.up;

        _direction_left_45 = new Vector2(-1f, 1f);
        _direction_right_45 = new Vector2(1f, 1f);

        _direction_left_30 = new Vector2(-Mathf.Pow(3f, .5f) / 3, 1f);
        _direction_right_30 = new Vector2(Mathf.Pow(3f, .5f) / 3, 1f);

        _direction_left_60 = new Vector2(-Mathf.Pow(3f, .5f), 1f);
        _direction_right_60 = new Vector2(Mathf.Pow(3f, .5f), 1f);
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused || Level == 0)
            return;

        Fire();
    }

    public void OnMakeVisible()
    {
        _leftRenderer.enabled = true;
        _rightRenderer.enabled = true;
    }

    public override void Fire()
    {
        if (TimeBetweenFire <= 0)
        {
            switch (Level)
            {
                case 1:

                    CreateProjectile(_left, _direction_up);
                    CreateProjectile(_right, _direction_up);

                    break;
                case 2:

                    CreateProjectile(_left, _direction_up);
                    CreateProjectile(_right, _direction_up);

                    CreateProjectile(_left, _direction_left_45);
                    CreateProjectile(_left, _direction_right_45);

                    CreateProjectile(_right, _direction_left_45);
                    CreateProjectile(_right, _direction_right_45);

                    break;

                case 3:

                    CreateProjectile(_left, _direction_up);
                    CreateProjectile(_right, _direction_up);

                    CreateProjectile(_left, _direction_left_30);
                    CreateProjectile(_right, _direction_left_30);

                    CreateProjectile(_left, _direction_right_30);
                    CreateProjectile(_right, _direction_right_30);

                    CreateProjectile(_left, _direction_left_60);
                    CreateProjectile(_right, _direction_left_60);

                    CreateProjectile(_left, _direction_right_60);
                    CreateProjectile(_right, _direction_right_60);

                    break;
            }

            TimeBetweenFire = StartTimeFire;
        }
        else
        {
            TimeBetweenFire -= Time.deltaTime;
        }

    }

    private void OnDisable()
    {
        UpgradeLevelUpper.MakeVisible -= OnMakeVisible;
    }
}
