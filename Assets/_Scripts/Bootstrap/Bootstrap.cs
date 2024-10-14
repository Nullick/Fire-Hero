using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;

    [SerializeField] private ScoreView _scoreView;

    [Header("Managers")]
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private PauseManager _pauseManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private ProjectileManager _projectileManager;

    [Header("Guns")]
    [SerializeField] private MainGun _mainGun;
    [SerializeField] private SideGun _sideGun;
    [SerializeField] private Laser _laser;
    [SerializeField] private Helpers _helpers;

    private List<IUpgradable> _weaponsUpgrades = new List<IUpgradable>();

    private void Awake()
    {
        Score score = new Score();

        _pauseManager.Initialize();
        _enemyManager.Initialize();
        _projectileManager.Initialize();

        Health playerHealth = new Health(1f);
        _player.Initialize(playerHealth);

        _mainGun.Initialize();
        _laser.Initialize();
        _sideGun.Initialize();
        _helpers.Initialize();

        _weaponsUpgrades.Add(_mainGun);
        _weaponsUpgrades.Add(_laser);
        _weaponsUpgrades.Add(_sideGun);
        _weaponsUpgrades.Add(_helpers);

        _upgradeManager.Initialize(score, _weaponsUpgrades);

        Timer complicatorTimer = new Timer(this);
        Complicator complicator = new Complicator(score, complicatorTimer);
        _spawner.Initialize(complicator);

        _scoreView.Initialize(score);

        //UpgradeLevelUpper upgradeLevelUpper = new UpgradeLevelUpper(_mainGun);
    }
}
