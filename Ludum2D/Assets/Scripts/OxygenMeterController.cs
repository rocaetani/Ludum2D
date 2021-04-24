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
        _oxygenPie.fillAmount = GameObjectAccess.Player.AirRatio;
    }
}
