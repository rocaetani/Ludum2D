using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public Color colorActive;

    public Color colorPressed;

    private SpriteRenderer _spriteRenderer;

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
    
    private bool _alreadyStarted ;

    public bool IsSwimUp;


    void Start()
    {
        _alreadyStarted = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    
    public void GetButton()
    {
        _keyButton = GameObjectAccess.KeysController.AddRandomUsedKey(_isLeft);
        BubbleText.text = _keyButton.KeyToString();

    }

    // Start is called before the first frame update
    void StartBubble()
    {
        _alreadyStarted = true;
        _isLeft = transform.position.x < GameObjectAccess.Player.transform.position.x;
        _timeItStarted = Time.time;
        GetButton();
        _popControl = false;
        
        //SpriteRenderer.color = colorActive;
        
        _animator.SetBool("Active", true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!_alreadyStarted)
        {
            _animator.SetBool("Active", false);
            if (IsSwimUp & GameObjectAccess.Player.GoingDirection() == 1)
            {
                if (GameObjectAccess.Player.transform.position.y < transform.position.y + 10)
                {
                    StartBubble();
                }
            }else if(!IsSwimUp & GameObjectAccess.Player.GoingDirection() == -1)
            {
                
                if (GameObjectAccess.Player.transform.position.y < transform.position.y + 10)
                {
                    StartBubble();
                }
            }
            
        }
        
        if (!_popControl & _alreadyStarted)
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
        GameObjectAccess.KeysController.ReleaseUsedKey(_keyButton);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider) {
        if(otherCollider.tag == "Player") {
            GameObjectAccess.Player.AddAirAmount(AdditionToAr);
            Pop();
        }
    }

}
