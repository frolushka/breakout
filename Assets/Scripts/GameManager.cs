using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Vector2 leftBottom;
    public Vector2 rightTop;

    public int ballSpawnQueue;

    [SerializeField] private GameConfiguration configuration;

    [Space]
    
    [SerializeField] private GameObject ballPrefab;
    
    [Space]

    [SerializeField] private Text ballsCountLabel;
    [SerializeField] private Text scoreLabel;

    [Space] 
    
    [SerializeField] private Transform ballSpawnPosition;
    
    [SerializeField] private EdgeCollider2D leftBorder;
    [SerializeField] private EdgeCollider2D rightBorder;
    [SerializeField] private EdgeCollider2D topBorder;

    private int _ballsCount;
    private int _score;

    private int BallsCount
    {
        get => _ballsCount;
        set
        {
            _ballsCount = value;
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
    }

    private void Start()
    {
        Score = 0;
        BallsCount = configuration.ballsPerGameCount;

        SetupPlayer();
        SpawnBall();
    }

    private void Update()
    {
        if (ballSpawnQueue <= 0) return;
        ballSpawnQueue--;
        SpawnBall();
    }

    private void SetupPlayer()
    {
        var player = FindObjectOfType<Paddle>();
        player.moveSpeed = configuration.paddleMoveSpeed;
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
        BallsCount--;
        var player = Instantiate(ballPrefab, ballSpawnPosition.position, Quaternion.identity);
        var ball = player.GetComponent<Ball>();
        ball.moveSpeed = configuration.ballMoveSpeed;
        Destroy(player, configuration.ballLifetime);
    }

    public void AddScoreForBlock(Block block)
    {
        switch (block)
        {
            case StandardBlock _:
                _score += configuration.pointsForStandardBlock;
                break;
            case BonusBlock _:
                _score += configuration.pointsForBonusBlock;
                break;
            case FreezerBlock _:
                _score += configuration.pointsForFreezerBlock;
                break;
            case SpeedupBlock _:
                _score += configuration.pointsForSpeedupBlock;
                break;
        }
    }
}
