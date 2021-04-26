using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        if(ScoreController.PublishScore(Score))
        {
            NewHS.SetActive(true);
            NewHSText.text = "New HighScore";
            LastHS = Score;
        }
        TextLastHS.text = $"HighScore: {ScoreController.GiveHS():0.00} meters";
    }

    public void nextLevel() {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    public void exitLevel() {
        SceneManager.LoadScene(0);
    }
}
