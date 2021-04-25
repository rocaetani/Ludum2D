using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenMeterController : MonoBehaviour
{
    public GameObject oxygenPie;
    private Image _oxygenPie;

    public Animator faceAnimator;

    void Start()
    {
        _oxygenPie = oxygenPie.GetComponent<Image>();
    }

    void Update()
    {
        _oxygenPie.fillAmount = GameObjectAccess.Player.AirRatio;
        faceAnimator.SetFloat("O2Meter", _oxygenPie.fillAmount);
    }
}
