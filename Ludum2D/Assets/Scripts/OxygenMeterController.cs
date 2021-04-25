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
        if(colorList.Count > 0) {
            oxygenPie.color = colorList[0];
        }
    }

    void Update()
    {
        oxygenPie.fillAmount = GameObjectAccess.Player.AirRatio;
        faceAnimator.SetFloat("O2Meter", oxygenPie.fillAmount);

        int state = getAnimatorState();
        if(state != -1 && colorList.Count > 0) {
            oxygenPie.color = colorList[Mathf.Min(colorList.Count - 1, state)];
        }
    }

    private int getAnimatorState() {
        AnimatorStateInfo state = faceAnimator.GetCurrentAnimatorStateInfo(0);
        if(state.IsName("CaraUI")) {
            return 0;
        } else if(state.IsName("Cara2UI")) {
            return 1;
        } else if(state.IsName("Cara3UI")) {
            return 2;
        } else if(state.IsName("Cara4UI")) {
            return 3;
        } else if(state.IsName("Cara5UI")) {
            return 4;
        } else if(state.IsName("Cara6UIMorte")) {
            return 5;
        } else {
            return -1;
        }
    }
}
