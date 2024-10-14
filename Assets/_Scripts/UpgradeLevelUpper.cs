using JetBrains.Annotations;
using System;
using System.Collections.Generic;

public class UpgradeLevelUpper
{
    public event Action UpgradeLevel;
    public static event Action MakeVisible;

    public void LevelUp(BoosterType boosterType, List<Weapon> upgrades)
    {
        switch (boosterType)
        {
            case BoosterType.MainGunBooster:

                for (int i = 0; i < upgrades.Count; i++)
                    if (upgrades[i].GetWeaponType() == WeaponType.MainGun)
                    {
                        upgrades[i].IncrementLevel();
                        UpgradeLevel?.Invoke();
                        break;
                    }
                break;

            case BoosterType.SideGunBooster:

                for (int i = 0; i < upgrades.Count; i++)
                    if (upgrades[i].GetWeaponType() == WeaponType.SideGun)
                    {
                        upgrades[i].IncrementLevel();
                        UpgradeLevel?.Invoke();
                        break;
                    }
                break;

            case BoosterType.LaserBooster:

                for (int i = 0; i < upgrades.Count; i++)
                    if (upgrades[i].GetWeaponType() == WeaponType.Laser)
                    {
                        upgrades[i].IncrementLevel();
                        UpgradeLevel?.Invoke();
                        break;
                    }
                break;

            case BoosterType.HelperBooster:

                for (int i = 0; i < upgrades.Count; i++)
                    if (upgrades[i].GetWeaponType() == WeaponType.Helper)
                    {
                        upgrades[i].IncrementLevel();
                        UpgradeLevel?.Invoke();
                        MakeVisible?.Invoke();
                        break;
                    }
                break;

            case BoosterType.DamageBooster:

                foreach (var upgrade in upgrades)
                    upgrade.EncreaseDamage();

                UpgradeLevel?.Invoke();
                break;

            case BoosterType.ProjectileSpeed:

                foreach (var upgrade in upgrades)
                    upgrade.EncreaseProjectileSpeed();

                UpgradeLevel?.Invoke();
                break;
        }
    }
}