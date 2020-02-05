using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Configuration", order = 1)]
public class GameConfiguration : ScriptableObject
{
    public float ballMoveSpeed;
    public float paddleMoveSpeed;
    
    [Space]
    
    public int pointsForStandardBlock;
    public int pointsForBonusBlock;
    public int pointsForFreezerBlock;
    public int pointsForSpeedupBlock;
}
