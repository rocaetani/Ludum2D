using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouWinScreen : MonoBehaviour
{
    public Text WinMessage;

    void Start()
    {
        WinMessage.text = $"You reached {GameObjectAccess.Player.Score:0.00} meters deep";
    }

}
