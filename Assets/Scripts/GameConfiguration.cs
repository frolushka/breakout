using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Configuration", order = 1)]
public class GameConfiguration : ScriptableObject
{
    public float paddleMoveSpeed;

    [Space] 
    
    public float ballMoveSpeed;
    public int ballLifetime;
    public int ballsPerGameCount;

    [Space] 
    
    public int pointsForStandardBlock;
    public int pointsForBonusBlock;
    public int pointsForFreezerBlock;
    public int pointsForSpeedupBlock;
}
