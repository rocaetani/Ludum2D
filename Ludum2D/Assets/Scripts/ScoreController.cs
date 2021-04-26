using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public float HighScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public bool PublishScore(float NewScore) //recebe a pontuação e retorna se foi recorde
    {
        if(NewScore > HighScore)
        {
            HighScore = NewScore;
            return true;
        }
        else
            return false;
    }

}
