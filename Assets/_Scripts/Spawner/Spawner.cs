using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
public class Spawner : MonoBehaviour 
{ 
    [Header("Time Data")]
    [SerializeField] private float _minSpawnTime = 5f;
    [SerializeField] private float _maxSpawnTime = 7f;
    [SerializeField, Header("Set automatically")] private float _spawnTime;

    [Header("Bricks Data")]
    [SerializeField] private Brick _default;
    [SerializeField] private MovableBrick _movable;
    [SerializeField] private float _minBrickHealth = 5f;
    [SerializeField] private float _maxBrickHealth = 15f;

    [Header("Other")]
    [SerializeField] private BoundsCheck _bnd;

    private float _brickPadding = 1.1f;

    private float _startMinSpawnTime;
    private float _startMaxSpawnTime;

    private float _startMinHealthBrick;
    private float _startMaxHealthBrick;

    private Timer _spawnTimer;

    private Vector2 _defaultBrickPosition = new Vector2(-2.2f, 0f);
    private Vector2 _movableBrickPosition = new Vector2(-4.4f, 0f);

    private Complicator _complicator;

    private bool _isMovablePossible = false;

    public void Initialize(Complicator complicator)
    {
        _bnd = GetComponent<BoundsCheck>();

        _startMinHealthBrick = _minBrickHealth;
        _startMaxHealthBrick = _maxBrickHealth;

        _spawnTimer = new Timer(this);

        _complicator = complicator;

        _spawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
        _startMinSpawnTime = _minSpawnTime;
        _startMaxSpawnTime = _maxSpawnTime;

        _spawnTimer.Set(_spawnTime);
        _spawnTimer.StartCountingTime();

        _complicator.ComplicatedByScore += OnComplicatedByScore;
        _complicator.ComplicatedByTime += SpawnBoss;
        _spawnTimer.TimeIsOver += OnSpawnTimerOver;
    }

    private void OnComplicatedByScore()
    {
        _isMovablePossible = true;

        _minSpawnTime -= _startMinSpawnTime * .1f;
        _maxSpawnTime -= _startMaxSpawnTime * .1f;

        _minBrickHealth += _startMinHealthBrick * .2f;
        _maxBrickHealth += _startMaxHealthBrick * .2f;

        _spawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);

        //_spawnTimer.Set(_spawnTime);
        //_spawnTimer.StartCountingTime();
        //скорость кубиков
    }

    private void OnSpawnTimerOver()
    {
        if (_isMovablePossible)
        {
            System.Random rnd = new System.Random();
            int choice = rnd.Next(1, 3);

            switch (choice)
            {
                case 1:
                    SpawnBricks(_defaultBrickPosition, 5, _default);
                    Debug.Log("default");
                    break;
                case 2:
                    SpawnBricks(_movableBrickPosition, 9, _movable);
                    Debug.Log("movable");
                    break;
            }
        }
        else
            SpawnBricks(_defaultBrickPosition, 5, _default);
    }

    private void SpawnBricks(Vector2 pos, int numb, Brick spawnBrick)
    {
        for(int i = 0; i < numb; i++)
        {
            Health healthBrick = new Health(Random.Range(_minBrickHealth, _maxBrickHealth));
            Brick brick = Instantiate<Brick>(spawnBrick);
            brick.Initialize(healthBrick, _maxBrickHealth);

            pos.y = _bnd.CamHeight + _brickPadding;
            brick.transform.position = pos;
            pos.x += _brickPadding;

            EnemyManager.Instance.AddList(brick);
        }

        _spawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
        _spawnTimer.Set(_spawnTime);
        _spawnTimer.StartCountingTime();
    }

    private void SpawnBoss()
    {

    }

    private void OnDestroy()
    {
        _spawnTimer.TimeIsOver -= OnSpawnTimerOver;
        _complicator.ComplicatedByScore -= OnComplicatedByScore;
        _complicator.ComplicatedByTime -= SpawnBoss;
    }
}
