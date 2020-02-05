using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector]
    public Vector2 leftBottom;
    [HideInInspector]
    public Vector2 rightTop;

    [HideInInspector]
    public int ballSpawnQueue;

    [Space]
    
    [SerializeField] private GameObject ballPrefab;
    
    [Space]

    [SerializeField] private Text ballsCountLabel;
    [SerializeField] private Text scoreLabel;

    [Space] 
    
    [SerializeField] private Transform ballSpawn;
    [SerializeField] private Transform blocksSpawn;
    
    [SerializeField] private EdgeCollider2D leftBorder;
    [SerializeField] private EdgeCollider2D rightBorder;
    [SerializeField] private EdgeCollider2D topBorder;
    
    [Space]
    
    [SerializeField] private GameObject standardBlockPrefab;
    [SerializeField] private GameObject bonusBlockPrefab;
    [SerializeField] private GameObject freezerBlockPrefab;
    [SerializeField] private GameObject speedupBlockPrefab;

    private int _ballsCount;
    private int _score;

    private bool _isGameEnded;

    private int BallsCount
    {
        get => _ballsCount;
        set
        {
            _ballsCount = value;
            if (_ballsCount <= 0)
                EndGame(false);
            if (ballsCountLabel)
                ballsCountLabel.text = $"Balls: {_ballsCount}";
        }
    }

    private int Score
    {
        get => _score;
        set
        {
            _score = value;
            if (scoreLabel)
                scoreLabel.text = $"Score: {_score}";
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        SetupBorders();

        StartCoroutine(GameConfiguration.ReadFromCSV());
    }

    private void Start()
    {
        Score = 0;
        BallsCount = GameConfiguration.ballsPerGameCount;
        
        SetupField();
        SetupPlayer();
        SpawnBall();

        StartCoroutine(SpawnBalls());
    }

    private void Update()
    {
        if (ballSpawnQueue <= 0) return;
        ballSpawnQueue--;
        SpawnBall();
    }

    private void SetupField()
    {
        const float offset = 0.5f;
        const int standard = 0;
        var bonus = standard + GameConfiguration.standardBlockProbability;
        var freezer = bonus + GameConfiguration.bonusBlockProbability;
        var speedup = freezer + GameConfiguration.freezerBlockProbability;
        var startPosition = blocksSpawn.position;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                
                var actualPosition = new Vector2(startPosition.x + (1 + offset) * (i - 2),startPosition.y + (.25f + offset) * (j - 1));
                var random = Random.Range(0, speedup + GameConfiguration.speedupBlockProbability);
                GameObject block;
                if (random >= standard && random < bonus)
                    block = Instantiate(standardBlockPrefab, actualPosition, Quaternion.identity);
                else if (random >= bonus && random < freezer)
                    block = Instantiate(bonusBlockPrefab, actualPosition, Quaternion.identity);
                else if (random >= freezer && random < speedup)
                    block = Instantiate(freezerBlockPrefab, actualPosition, Quaternion.identity);
                else
                    block = Instantiate(speedupBlockPrefab, actualPosition, Quaternion.identity);
                block.transform.SetParent(blocksSpawn);
            }
        }
    }

    private void SetupPlayer()
    {
        var player = FindObjectOfType<Paddle>();
        player.moveSpeed = GameConfiguration.paddleMoveSpeed;
    }
    
    private void SetupBorders()
    {
        var cam = Camera.main;
        if (cam == null) return;
        
        leftBottom = cam.ScreenToWorldPoint(Vector3.zero);
        leftBottom.x = leftBottom.y / 9 * 16;
        rightTop = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        rightTop.x = rightTop.y / 9 * 16;
        
        // Debug.Log($"Left bottom: {leftBottom}");
        // Debug.Log($"Right top: {rightTop}");
        
        var rightBottom = new Vector2(rightTop.x, leftBottom.y);
        var leftTop = new Vector2(leftBottom.x, rightTop.y);
        
        leftBorder.points = new [] { leftBottom, leftTop };
        rightBorder.points = new [] { rightBottom, rightTop };
        topBorder.points = new [] { leftTop, rightTop };
    }
    
    private void SpawnBall()
    {
        if (BallsCount <= 0) return;
        var player = Instantiate(ballPrefab, ballSpawn.position, Quaternion.identity);
        var ball = player.GetComponent<Ball>();
        ball.moveSpeed = GameConfiguration.ballMoveSpeed;
        Destroy(player, GameConfiguration.ballLifetime);
    }

    private void EndGame(bool isWinner)
    {
        _isGameEnded = true;
        Time.timeScale = 0;
    }

    private IEnumerator SpawnBalls()
    {
        while (true)
        {
            var delay = Random.Range(GameConfiguration.minBallSpawnDelay, GameConfiguration.maxBallSpawnDelay);
            yield return new WaitForSeconds(delay);
            if (_isGameEnded) yield break;
            SpawnBall();
        }
    }

    public void BallLeftScreen()
    {
        BallsCount--;
        SpawnBall();
    }

    public void AddScoreForBlock(Block block)
    {
        switch (block)
        {
            case StandardBlock _:
                Score += GameConfiguration.pointsForStandardBlock;
                break;
            case BonusBlock _:
                Score += GameConfiguration.pointsForBonusBlock;
                break;
            case FreezerBlock _:
                Score += GameConfiguration.pointsForFreezerBlock;
                break;
            case SpeedupBlock _:
                Score += GameConfiguration.pointsForSpeedupBlock;
                break;
        }
    }
}
