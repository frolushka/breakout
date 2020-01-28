using UnityEngine;

public static class ScreenUtility
{
    public static float LeftSideCoordinate { get; private set; }
    public static float RightSideCoordinate { get; private set; }
    public static float BottomSideCoordinate { get; private set; }
    public static float TopSideCoordinate { get; private set; }
    
    public static void Setup()
    {
        var camera = Camera.main;
        var leftBottomCorner = camera.ScreenToWorldPoint(new Vector3(0, 0));
        var rightTopCorner = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        LeftSideCoordinate = leftBottomCorner.x;
        RightSideCoordinate = rightTopCorner.x;
        BottomSideCoordinate = leftBottomCorner.y;
        TopSideCoordinate = rightTopCorner.y;
    }
}
