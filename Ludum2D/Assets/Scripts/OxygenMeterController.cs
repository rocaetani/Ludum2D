using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenMeterController : MonoBehaviour
{

   private Image _oxygenPie;

    void Start()
    {
        _oxygenPie = GetComponent<Image>();
    }

    void Update()
    {
        Debug.Log(GameObjectAccess.Player.AirRatio);
        _oxygenPie.fillAmount = GameObjectAccess.Player.AirRatio;
    }
}
