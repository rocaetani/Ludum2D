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
    private Image _buttonImage;
    private TextMeshPro _text;

    public Sprite buttonActivated;
    public Sprite buttonNotPressed;
    public Sprite buttonPressed;
    
    private bool _isPressed;
    private bool _isToPress;
    private bool _isFastPress;
    
    // Start is called before the first frame update
    void Start()
    {
        _isPressed = false;
        _isFastPress = false;
        _isToPress = false;

        _buttonImage = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<TextMeshPro>();

        _text.text = key + "";
    }

    // Update is called once per frame
    void Update()
    {
        VerifyIsPressed();
        if (_isToPress)
        {
            if (!_isPressed)
            {
                GameObjectAccess.Player.AmountToDecrease += 1;
                _buttonImage.sprite = buttonNotPressed;
            }
            else
            {
                _buttonImage.sprite = buttonPressed;
            }


        }

        
    }


    public int IntKey()
    {
       return (int) key;
    }

    public void VerifyIsPressed()
    {
        if (Input.GetKey(key))
        {
            _isPressed = true;
        }
    }
}
