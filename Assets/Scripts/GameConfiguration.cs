using System.IO;
using UnityEngine;

public static class GameConfiguration
{
    public static float paddleMoveSpeed;
    
    public static float ballMoveSpeed;
    public static int ballLifetime;
    public static int ballsPerGameCount;
    
    public static float minBallSpawnDelay;
    public static float maxBallSpawnDelay;
    
    public static int pointsForStandardBlock;
    public static int pointsForBonusBlock;
    public static int pointsForFreezerBlock;
    public static int pointsForSpeedupBlock;

    public static int standardBlockProbability;
    public static int bonusBlockProbability;
    public static int freezerBlockProbability;
    public static int speedupBlockProbability;

    public static void ReadFromCSV()
    {
        string path = Application.dataPath + "/configuration.csv";
        if (!File.Exists(path))
            throw new FileNotFoundException();
        using (var sr = new StreamReader(path))
        {
            var tokens = sr.ReadToEnd().Split(';');
            paddleMoveSpeed = float.Parse(tokens[0]);
            ballMoveSpeed = float.Parse(tokens[1]);
            ballLifetime = int.Parse(tokens[2]);
            ballsPerGameCount = int.Parse(tokens[3]);
            minBallSpawnDelay = float.Parse(tokens[4]);
            maxBallSpawnDelay = float.Parse(tokens[5]);
            pointsForStandardBlock = int.Parse(tokens[6]);
            pointsForBonusBlock = int.Parse(tokens[7]);
            pointsForFreezerBlock = int.Parse(tokens[8]);
            pointsForSpeedupBlock = int.Parse(tokens[9]);
            standardBlockProbability = int.Parse(tokens[10]);
            bonusBlockProbability = int.Parse(tokens[11]);
            freezerBlockProbability = int.Parse(tokens[12]);
            speedupBlockProbability = int.Parse(tokens[13]);
        }
    }

    // public void WriteToCSV()
    // {
    //     string path = Application.dataPath + "/configuration.csv";
    //     using (var sr = new StreamWriter(path))
    //     {
    //         sr.Write(paddleMoveSpeed + ";");
    //         sr.Write(ballMoveSpeed + ";");
    //         sr.Write(ballLifetime + ";");
    //         sr.Write(ballsPerGameCount + ";");
    //         sr.Write(minBallSpawnDelay + ";");
    //         sr.Write(maxBallSpawnDelay + ";");
    //         sr.Write(pointsForStandardBlock + ";");
    //         sr.Write(pointsForBonusBlock + ";");
    //         sr.Write(pointsForFreezerBlock + ";");
    //         sr.Write(pointsForSpeedupBlock + ";");
    //         sr.Write(standardBlockProbability + ";");
    //         sr.Write(bonusBlockProbability + ";");
    //         sr.Write(freezerBlockProbability + ";");
    //         sr.Write(speedupBlockProbability + ";");
    //     }
    // }
}
