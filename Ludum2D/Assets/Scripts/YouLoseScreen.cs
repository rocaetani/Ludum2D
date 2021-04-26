using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLoseScreen : MonoBehaviour
{
    public void retryLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void exitLevel() {
        SceneManager.LoadScene(0);
    }
}
