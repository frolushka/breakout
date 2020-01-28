using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EdgeCollider2D leftBorder;
    [SerializeField] private EdgeCollider2D rightBorder;
    [SerializeField] private EdgeCollider2D bottomBorder;
    [SerializeField] private EdgeCollider2D topBorder;
    
    private void Awake()
    {
        ScreenUtility.Setup();
        SetupBorders();
    }

    private void SetupBorders()
    {
        var leftBottom = new Vector2(ScreenUtility.LeftSideCoordinate, ScreenUtility.BottomSideCoordinate);
        var rightBottom = new Vector2(ScreenUtility.RightSideCoordinate, ScreenUtility.BottomSideCoordinate);
        var leftTop = new Vector2(ScreenUtility.LeftSideCoordinate, ScreenUtility.TopSideCoordinate);
        var rightTop = new Vector2(ScreenUtility.RightSideCoordinate, ScreenUtility.TopSideCoordinate);
        
        leftBorder.points = new [] { leftBottom, leftTop };
        rightBorder.points = new [] { rightBottom, rightTop };
        bottomBorder.points = new [] { leftBottom, rightBottom };
        topBorder.points = new [] { leftTop, rightTop };
    }
}
