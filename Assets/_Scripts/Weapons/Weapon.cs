using UnityEngine;

public abstract class Weapon : MonoBehaviour, IUpgradable
{
    [Header("Projectile Data")]
    [SerializeField] protected Projectile Projectile;
    [SerializeField] protected float ProjectileSpeed;
    [SerializeField] protected float StartProjectileSpeed;

    [SerializeField] protected float Damage;
    [SerializeField] protected float StartDamage;


    [Header("Level Data")]
    [SerializeField] protected int Level;
    [SerializeField] protected int MaxLevel;

    [Header("Shot Data")]
    [SerializeField] protected float StartTimeFire;
    [SerializeField] protected float TimeBetweenFire;

    [Header("Other")]
    [SerializeField] protected WeaponType WeaponType;

    public abstract void Fire();

    protected void CreateProjectile(Transform position, Vector2 projectileDirection)
    {
        Projectile proj = Instantiate(Projectile);
        proj.Initialize(Damage, ProjectileSpeed, projectileDirection);
        ProjectileManager.Instance.AddList(proj);

        proj.transform.position = position.position;
    }

    public void IncrementLevel()
    {
        if (IsMaxLevel())
        {
            EncreaseDamage();
            return;
        }

        if (Level == 0)
        {
            Level = 1;
            return;
        }

        Level++;
        Damage += StartDamage * .1f;
        TimeBetweenFire -= TimeBetweenFire * .1f;
        ProjectileSpeed += StartProjectileSpeed * .1f;
    }

    public void EncreaseDamage()
    {
        if (Level > 0)
            Damage += StartDamage * .15f;
    }

    public void EncreaseProjectileSpeed()
    {
        if (Level > 0)
            ProjectileSpeed += StartProjectileSpeed * .15f;
    }

    public bool IsMaxLevel()
    {
        return Level >= MaxLevel;
    }

    public int GetLevel()
    {
        return Level;
    }

    public WeaponType GetWeaponType()
    {
        return WeaponType;
    }
}
