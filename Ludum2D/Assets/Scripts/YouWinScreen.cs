using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouWinScreen : MonoBehaviour
{
    public Text WinMessage;
    public float Score;
    public float LastHS = 0f;
    public Text NewHSText;
    public GameObject NewHS;
    public Text TextLastHS;

    void Start()
    {
        Score = GameObjectAccess.Player.Score;
        WinMessage.text = $"You reached {Score:0.00} meters deep";

        if(Score > LastHS)
        {
            NewHS.SetActive(true);
            NewHSText.text = "New HighScore";
            LastHS = Score;
        }
        TextLastHS.text = $"HighScore: {LastHS:0.00} meters";
    }

}
