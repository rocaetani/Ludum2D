using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Color colorActive;

    public Color colorPressed;

    public TMP_Text Text;

    private KeyButton _keyButton;

    public float AdditionToAr;

    public int PressesNeeded;

    private int _numberOfPresses;

    public float TimeToDisappear;

    private float _timeItStarted;

    public void GetButton()
    {
        bool isLeft = transform.position.x < 0;
        _keyButton = GameObjectAccess.KeysController.AddRandomKeyToBubble(isLeft);
        Text = _keyButton.Text;
        
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        _timeItStarted = Time.time;
        GetButton();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_keyButton.key))
        {
            
        }
    }
}
