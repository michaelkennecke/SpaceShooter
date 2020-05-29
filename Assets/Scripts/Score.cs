using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    static int score;

    static Score()
    {
        score = 0;
    }

    public static void AddScore(int value)
    {
        score += value;
    }
    public static void LowerScore(int value)
    {
        score -= value;
    }
    public static int GetScore()
    {
        return score;
    }

    public static void Reset()
    {
        score = 0;
    }

}
