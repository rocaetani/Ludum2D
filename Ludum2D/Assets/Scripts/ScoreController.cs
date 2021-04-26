using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController
{
    public static float HighScore = 0;

    public static bool PublishScore(float NewScore) //recebe a pontuação e retorna se foi recorde
    {
        if(NewScore > HighScore)
        {
            HighScore = NewScore;
            return true;
        }
        else
            return false;
    }

    public static float GiveHS()
    {
        return HighScore;
    }

}
