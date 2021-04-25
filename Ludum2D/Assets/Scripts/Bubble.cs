﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public Color colorActive;

    public Color colorPressed;
    
    public SpriteRenderer Sprite;

    public TMP_Text BubbleText;

    private KeyButton _keyButton;

    public float AdditionToAr;

    public int PressesNeeded;

    private int _numberOfPresses;

    public float TimeToDisappear;

    private float _timeItStarted;

    private bool _popControl;
    
    public void GetButton()
    {
        bool isLeft = transform.position.x < 0;
        _keyButton = GameObjectAccess.KeysController.AddRandomKeyToBubble(isLeft);
        BubbleText.text = _keyButton.KeyToString();
        
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        _timeItStarted = Time.time;
        GetButton();
        _popControl = false;
        Sprite.color = colorActive;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_popControl)
        {
            
            
            if (Input.GetKeyDown(_keyButton.key))
            {
                _numberOfPresses += 1;
                Sprite.color = colorPressed;
                if (_numberOfPresses == PressesNeeded)
                {
                    GameObjectAccess.Player.AddAirAmount(AdditionToAr);
                    Pop();
                }
            }
            else
            {
                Sprite.color = colorActive;
            }

            if (!_popControl)
            {
                if (TimeToDisappear < Time.time - _timeItStarted)
                {
                    Pop();
                }
                
                if (Mathf.Abs(transform.position.y - GameObjectAccess.Player.transform.position.y) > 5)
                {
                    Pop();
                }
                

            }
        }
    }

    private void Pop()
    {
        _popControl = true;
        //anim of popping
        Destroy(gameObject);
    }
    
}
