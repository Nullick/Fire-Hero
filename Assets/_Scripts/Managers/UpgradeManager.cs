using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<BoosterDisplay> _boosters;
    [SerializeField] private List<BoosterDisplay> _boostersOnDisplay = new List<BoosterDisplay>();
    [SerializeField] private Canvas _canvas;

    [SerializeField] private List<Weapon> _upgradesWeapon = new List<Weapon>();

    private Score _score;
    private UpgradeLevelUpper _upgradeLevelUpper;

    public void Initialize(Score score, List<IUpgradable> upgrades)
    {
        _upgradeLevelUpper = new UpgradeLevelUpper();
        _score = score;

        BoosterDisplay.BoosterClicked += OnBoosterClicked;
        _score.Changed += OnScoreChanged;
        _upgradeLevelUpper.UpgradeLevel += OnUpgradeLevel;

        foreach (var upgrade in upgrades)
        {
            _upgradesWeapon.Add((Weapon)upgrade);
        }
    }

    private void OnBoosterClicked(BoosterType boosterType)
    {
        _upgradeLevelUpper.LevelUp(boosterType, _upgradesWeapon);
    }

    public void OnUpgradeLevel()
    {
        PauseManager.Instance.SetPaused(false);

        foreach (var booster in _boostersOnDisplay)
        {
            Destroy(booster.gameObject);
        }

        _boostersOnDisplay.Clear();
    }

    private void OnScoreChanged()
    {
        if (_score.Value == 10  && _score.Value != 0)
        {
            PauseManager.Instance.SetPaused(true);
            BoosterCreate();
        }
    }

    private void BoosterCreate()
    {
        Vector2 pos = new Vector2(-335.0f, 0.0f);

        for (int i = 0; i < 3; i++)
        {
            int boostNumb = Random.Range(0, _boosters.Count);

            BoosterDisplay boost = Instantiate<BoosterDisplay>(_boosters[boostNumb]);
            boost.transform.SetParent(_canvas.transform);
            boost.transform.localPosition = pos;
            pos.x += 335.0f;

            _boostersOnDisplay.Add(boost);
        }
    }

    private void OnDestroy()
    {
        _score.Changed -= OnScoreChanged;
        BoosterDisplay.BoosterClicked -= OnBoosterClicked;
        _upgradeLevelUpper.UpgradeLevel -= OnUpgradeLevel;
    }
}
