using UnityEngine;

public static class ScoreManager
{
    public static int score = 0;

    public static void ResetScore()
    {
        score = 0;
    }

    public static void AddScore(int value)
    {
        score += value;
    }
}
