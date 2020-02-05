using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

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

    public static IEnumerator ReadFromCSV()
    {
        var path = Path.Combine(Application.streamingAssetsPath, "configuration.csv");
        string[] tokens;
#if UNITY_EDITOR
        tokens = Encoding.UTF8.GetString(File.ReadAllBytes(path)).Split(';');
#else
        var www = new UnityWebRequest(path)
        {
            downloadHandler = new DownloadHandlerBuffer()
        };
        yield return www.SendWebRequest();
        if (www.isHttpError || www.isNetworkError)
        {
            Debug.Log(www.error);
        yield break;
        }
        else
            tokens = Encoding.UTF8.GetString(www.downloadHandler.data).Split(';');
#endif
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
        yield return null;
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
