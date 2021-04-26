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

    private Animator _animator;

    private bool _isLeft;
    
    public void GetButton()
    {
        _keyButton = GameObjectAccess.KeysController.AddRandomKeyToBubble(_isLeft);
        BubbleText.text = _keyButton.KeyToString();

    }

    // Start is called before the first frame update
    void OnEnable()
    {
        _isLeft = transform.position.x < GameObjectAccess.Player.transform.position.x;
        _timeItStarted = Time.time;
        GetButton();
        _popControl = false;
        Sprite.color = colorActive;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_popControl)
        {


            if (Input.GetKeyDown(_keyButton.key))
            {
                _numberOfPresses++;
                _animator.SetBool("Pressed", true);
                GameObjectAccess.Player.MoveSideways(gameObject);
            }
            else
            {
                _animator.SetBool("Pressed", false);
            }

            if (!_popControl)
            {
                if (TimeToDisappear < Time.time - _timeItStarted)
                {
                    Pop();
                }

                if (Mathf.Abs(transform.position.y - GameObjectAccess.Player.transform.position.y) > 15)
                {
                    DestroyBubble();
                }


            }
        }
    }

    private void Pop()
    {
        // TODO som de bolha estourando
        _popControl = true;
        _animator.SetBool("Pop", true);
        BubbleText.text = "";
        //Destroy(gameObject);
    }

    private void DestroyBubble()
    {
        GameObjectAccess.KeysController.ReleaseKeyToPress(_keyButton);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider) {
        if(otherCollider.tag == "Player") {
            GameObjectAccess.Player.AddAirAmount(AdditionToAr);
            Pop();
        }
    }

}
