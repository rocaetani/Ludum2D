using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenMeterController : MonoBehaviour
{
    public Image oxygenPie;

    public Animator faceAnimator;

    public List<Color> colorList;

    void Start()
    {
    }

    void Update()
    {
        oxygenPie.fillAmount = GameObjectAccess.Player.AirRatio;
        faceAnimator.SetFloat("O2Meter", oxygenPie.fillAmount);
    }
}
