using UnityEngine;
using UnityEngine.UI;

public class HealthCount : MonoBehaviour
{
    [SerializeField] private Text _healthText;
    [SerializeField] private Health _health;

    public void Initialize(Health health)
    {
        _health = health;
        _healthText.text = Mathf.Floor(health.Value).ToString();
        _health.Changed += OnHealthChanged;
    }

    private void OnHealthChanged(float value)
    {
        int intValue = (int)value;
        _healthText.text = intValue.ToString();
    }

    private void OnDestroy()
    {
        _health.Changed -= OnHealthChanged;
    }
}
