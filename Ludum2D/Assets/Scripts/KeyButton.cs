using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{

    public KeyCode key;
    public Image ButtonImage;
    public TMP_Text Text;


    public Sprite buttonActivated;
    public Sprite buttonNotPressed;
    public Sprite buttonPressed;

    public Sprite buttonActivating;
    
    public float timePiscando;
    public int numPiscadas;

    private bool _isPressed;
    private bool _isToPress;
    private bool _isFastPress;

    private bool controlPlayerAir;
    
    // Start is called before the first frame update
    void Start()
    {
        controlPlayerAir = true;
        _isPressed = false;
        _isFastPress = false;
        _isToPress = false;
        

        ButtonImage = GetComponentInChildren<Image>();

        ButtonImage.enabled = false;
        Text.enabled = false;

        String keyString = key + "";
        keyString = keyString.Replace("Alpha", "");
        Text.text = keyString;
    }

    // Update is called once per frame
    void Update()
    {
        VerifyIsPressed();
        if (_isToPress)
        {
            if (!_isPressed)
            {
                if (controlPlayerAir)
                {
                    controlPlayerAir = false;
                    GameObjectAccess.Player.DecreasePerSecond += GameObjectAccess.Player.DecreasePerSecondPerButton;
                }
                ButtonImage.sprite = buttonNotPressed;
                ButtonImage.enabled = true;
                Text.enabled = true;
            }
            else
            {
                if (!controlPlayerAir)
                {
                    controlPlayerAir = true;
                    GameObjectAccess.Player.DecreasePerSecond -= GameObjectAccess.Player.DecreasePerSecondPerButton;
                }
                ButtonImage.sprite = buttonPressed;
                ButtonImage.enabled = true;
                Text.enabled = true;
            }
        }
    }

    public void SetToPress()
    {
        StartCoroutine(IsComming());
        //_isToPress = true;
    }

    IEnumerator IsComming()
    {
        
        ButtonImage.enabled = true;
        Text.enabled = true; //da pra tirar o texto aqui tbm se for melhor
        for(int i = 0; i < numPiscadas; i++)
        {
        ButtonImage.sprite = buttonActivating;
        yield return new WaitForSeconds(timePiscando);
        ButtonImage.sprite = buttonActivated;
        yield return new WaitForSeconds(timePiscando);
        }
        

         //yield return new WaitForSeconds(3f);

        _isToPress = true;
    }

    public int IntKey()
    {
       return (int) key;
    }

    public String KeyToString()
    {
        String keyString = key + "";
        keyString = keyString.Replace("Alpha", "");
        return keyString;
    }

    public void VerifyIsPressed()
    {
        if (Input.GetKey(key))
        {
            _isPressed = true;
        }
        else
        {
            _isPressed = false;
        }
    }
}
