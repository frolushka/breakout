using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public Vector2 LeftBottom { get; private set; }
    public Vector2 RightTop { get; private set; }

    [SerializeField] private GameConfiguration configuration;

    [Space]
    
    // [SerializeField] private SpriteRenderer gameFieldBackground;
    
    [SerializeField] private EdgeCollider2D leftBorder;
    [SerializeField] private EdgeCollider2D rightBorder;
    [SerializeField] private EdgeCollider2D bottomBorder;
    [SerializeField] private EdgeCollider2D topBorder;
    
    private int _score;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        SetupBorders();
    } 

    private void SetupBorders()
    {
        var cam = Camera.main;
        if (cam == null) return;
        
        LeftBottom = cam.ScreenToWorldPoint(Vector3.zero);
        RightTop = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        var rightBottom = new Vector2(RightTop.x, LeftBottom.y);
        var leftTop = new Vector2(LeftBottom.x, RightTop.y);
        
        leftBorder.points = new [] { LeftBottom, leftTop };
        rightBorder.points = new [] { rightBottom, RightTop };
        bottomBorder.points = new [] { LeftBottom, rightBottom };
        topBorder.points = new [] { leftTop, RightTop };
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
