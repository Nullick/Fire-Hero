using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    private Text _scoreText;
    private Score _score;

    public void Initialize(Score score)
    {
        _scoreText = GetComponent<Text>();
        _score = score;
        _score.ChangedValue += OnChanged;
        EnemyManager.EnemyDied += OnDied;
    }
        
    private void OnChanged(int value)
    {
        _scoreText.text = value.ToString();
    }

    private void OnDied()
    {
        _score.Change();
        OnChanged(_score.Value);
    }

    private void OnDestroy()
    {
        _score.ChangedValue -= OnChanged;
        EnemyManager.EnemyDied -= OnDied;
    }
}