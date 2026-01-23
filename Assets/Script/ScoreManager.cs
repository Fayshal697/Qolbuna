using UnityEngine;

public static class ScoreManager
{
    private static int score = 0;

    public static int Score => score;

    public static void AddScore(int value)
    {
        score += value;
    }

    public static void ResetScore()
    {
        score = 0;
    }
}
