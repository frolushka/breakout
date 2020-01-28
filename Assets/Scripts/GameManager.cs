using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public Vector2 GameFieldSize { get; private set; }

    [SerializeField] private SpriteRenderer gameFieldBackground;
    
    [SerializeField] private EdgeCollider2D leftBorder;
    [SerializeField] private EdgeCollider2D rightBorder;
    [SerializeField] private EdgeCollider2D bottomBorder;
    [SerializeField] private EdgeCollider2D topBorder;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        GameFieldSize = Vector3.Scale(gameFieldBackground.sprite.bounds.size, gameFieldBackground.transform.localScale);
        SetupBorders();
    } 

    private void SetupBorders()
    {
        var p = gameFieldBackground.transform.position;
        var leftBottom = new Vector2(p.x - GameFieldSize.x / 2, p.y - GameFieldSize.y / 2);
        var rightBottom = new Vector2(p.x + GameFieldSize.x / 2, p.y - GameFieldSize.y / 2);
        var leftTop = new Vector2(p.x - GameFieldSize.x / 2, p.y + GameFieldSize.y / 2);
        var rightTop = new Vector2(p.x + GameFieldSize.x / 2, p.y + GameFieldSize.y / 2);
        
        leftBorder.points = new [] { leftBottom, leftTop };
        rightBorder.points = new [] { rightBottom, rightTop };
        bottomBorder.points = new [] { leftBottom, rightBottom };
        topBorder.points = new [] { leftTop, rightTop };
    }
}
