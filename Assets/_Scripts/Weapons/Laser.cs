using UnityEngine;
using UnityEngine.UIElements;

public class Laser : Weapon
{
    [SerializeField] private Transform _position;

    [Header("Directions")]
    [SerializeField] private Transform _direction_left_30;
    [SerializeField] private Transform _direction_right_30;

    private LineRenderer _lineRenderer;

    //private Vector2 _direction_up;

    //private Vector2 _direction_left_45;
    //private Vector2 _direction_right_45;

    //private Vector2 _direction_left_30;
    //private Vector2 _direction_right_30;

    //private Vector2 _direction_left_60;
    //private Vector2 _direction_right_60;

    private void Update()
    {
        if (PauseManager.Instance.IsPaused || Level == 0)
            return;

        Fire();
    }

    public void Initialize()
    {
        Level = 0;
        MaxLevel = 3;

        StartTimeFire = .3f;
        TimeBetweenFire = StartTimeFire;

        ProjectileSpeed = 5f;
        StartProjectileSpeed = ProjectileSpeed;

        Damage = .3f;
        StartDamage = Damage;

        WeaponType = WeaponType.Laser;

        _lineRenderer = GetComponent<LineRenderer>();

        //InitializeDirections();
    }

    //private void InitializeDirections()
    //{
    //    _direction_up = Vector2.up;

    //    _direction_left_45 = new Vector2(-1f, 1f);
    //    _direction_right_45 = new Vector2(1f, 1f);

    //    _direction_left_30 = new Vector2(-Mathf.Pow(3f, .5f) / 3, 1f);
    //    _direction_right_30 = new Vector2(Mathf.Pow(3f, .5f) / 3, 1f);

    //    _direction_left_60 = new Vector2(-Mathf.Pow(3f, .5f), 1f);
    //    _direction_right_60 = new Vector2(Mathf.Pow(3f, .5f), 1f);
    //}

    public override void Fire()
    {
        if(TimeBetweenFire <= 0)
        {
            switch (Level)
            {
                case 1:

                    CreateLaser();
                    //CreateProjectile(_position, _direction_up);

                    break;

                case 2:

                    //CreateProjectile(_position, _direction_up);

                    //CreateProjectile(_position, _direction_left_45);
                    //CreateProjectile(_position, _direction_right_45);

                    break;

                case 3:

                    //CreateProjectile(_position, _direction_up);

                    //CreateProjectile(_position, _direction_left_30);
                    //CreateProjectile(_position, _direction_right_30);

                    //CreateProjectile(_position, _direction_left_60);
                    //CreateProjectile(_position, _direction_right_60);

                    break;
            }

            TimeBetweenFire = StartTimeFire;
        }
        else
        {
            TimeBetweenFire -= Time.deltaTime;
        }
    }

    private void CreateLaser()
    {
        _lineRenderer.SetPosition(0, _position.position);
        //_lineRenderer.SetPosition(1, _direction_left_30);

        RaycastHit hit;

        if(Physics.Raycast(transform.position, _direction_left_30.position, out hit))
        {
            if (hit.collider)
            {
                _lineRenderer.SetPosition(1, hit.point);
                Debug.Log("Laser shot");
            }
        }
        else
        {
            _lineRenderer.SetPosition(1, _direction_left_30.position);
        }
    }
}
