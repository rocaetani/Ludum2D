﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Arrow : MonoBehaviour
{
    public SpriteRenderer Sprite;

    public TMP_Text Text;

    private KeyButton _keyButton;

    public int PressesNeeded;

    private int _numberOfPresses;

    public float TimeToDisappear;

    private float _timeItStarted;

    private bool _exitControl;

    private Animator _animator;

    private bool _isLeft;

    private bool _alreadyStarted ;


    void Start()
    {
        _alreadyStarted = false;
    }

    public void GetButton()
    {
        _keyButton = GameObjectAccess.KeysController.AddRandomKeyToBubble(_isLeft);
        Text.text = _keyButton.KeyToString();

    }

    // Start is called before the first frame update
    void StartArrow()
    {
        _alreadyStarted = true;
        _isLeft = transform.position.x < GameObjectAccess.Player.transform.position.x;
        _timeItStarted = Time.time;
        GetButton();
        _exitControl = false;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_alreadyStarted)
        {
            if (GameObjectAccess.Player.transform.position.y < transform.position.y + 10)
            {
                StartArrow();
            }
        }

        if (!_exitControl)
        {


            if (Input.GetKeyDown(_keyButton.key))
            {
                //_animator.SetBool("Pressed", true);
                GameObjectAccess.Player.MoveSideways(gameObject);
            }
            else
            {
                //_animator.SetBool("Pressed", false);
            }

            if (!_exitControl)
            {
                if (GameObjectAccess.Player.transform.position.x < transform.position.x & _isLeft)
                {
                    Exit();
                }
                else if (GameObjectAccess.Player.transform.position.x > transform.position.x & !_isLeft)
                {
                    Exit();
                }

                if (TimeToDisappear < Time.time - _timeItStarted)
                {
                    Exit();
                }

                if (Mathf.Abs(transform.position.y - GameObjectAccess.Player.transform.position.y) > 15)
                {
                    GameObjectAccess.KeysController.ReleaseKeyToPress(_keyButton);
                    Destroy(gameObject);
                }


            }
        }
    }

    private void Exit()
    {
        // TODO som de bolha estourando
        _exitControl = true;
        //_animator.SetBool("Exit", true);
        Text.text = "";
        //Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider) {
        if(otherCollider.tag == "Player") {
            Exit();
        }
    }

}
