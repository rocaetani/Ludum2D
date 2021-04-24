using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class KeysPressedController : MonoBehaviour
{

    private List<KeyButton> _keyToPressList;

    private int _lastTimeKeyAdded;

    private List<KeyButton> _freeLeftKeyList;
    
    private List<KeyButton> _freeRightKeyList;
    void Start()
    {
        _keyToPressList = new List<KeyButton>();
        _freeLeftKeyList = new List<KeyButton>();
        _freeRightKeyList = new List<KeyButton>();
        InitFreeKeysLists();
    }

    //48-57  97-122
    void Update()
    {
        CreateKeyToPressByTime();
    }

    private void CreateKeyToPressByTime()
    {
        if (TruncateTime() % 3 == 0 & TruncateTime() != _lastTimeKeyAdded)
        {
            _lastTimeKeyAdded = TruncateTime();
            AddKeyToPress();
        }
    }

    // 0-36  Numeros e Letras
    public KeyButton RandomizeKey()
    {
        int i = Random.Range(0, _freeLeftKeyList.Count + _freeRightKeyList.Count);
        KeyButton freeKey;
        if (i < _freeLeftKeyList.Count)
        {
            freeKey = _freeLeftKeyList[i];
            _freeLeftKeyList.RemoveAt(i);
        }
        else
        {
            i -= _freeLeftKeyList.Count;
            freeKey = _freeRightKeyList[i];
            _freeRightKeyList.RemoveAt(i);
        }
        
        return freeKey;
    }

    public void ReleaseKeyToPress(KeyButton keyButton)
    {
        if (VerifyButtonSide(keyButton.key))
        {
            _freeLeftKeyList.Add(keyButton);
        }
        else
        {
            _freeRightKeyList.Add(keyButton);
        }
    }
    

    public void AddKeyToPress()
    {
        KeyButton key = RandomizeKey();
        key.SetToPress();
        _keyToPressList.Add(key);
    }

    private void InitFreeKeysLists()
    {
        foreach (KeyButton keyButton in GetComponentsInChildren<KeyButton>())
        {
            if (VerifyButtonSide(keyButton.key))
            {
                _freeLeftKeyList.Add(keyButton);
            }
            else
            {
                _freeRightKeyList.Add(keyButton);
            }
        }
    }

    //return true=Left false=Right 
    private bool VerifyButtonSide(KeyCode keyCode)
    {
        String KeyString = keyCode + "";
        KeyString = KeyString.Replace("Alpha", "");
        String leftSide = "12345QWERTASDFGZXCV";
        if (leftSide.Contains(KeyString))
        {
            return true;
        }

        return false;
    }

    private int TruncateTime()
    {
        return Mathf.FloorToInt(Time.time);
    }
    
    void OnGUI()
    {
        int space = 0;
        GUI.color = Color.red;
        foreach (KeyButton key in _keyToPressList)
        {
            GUI.Label(new Rect(space, 490, 60, 30), key.Text + "");
            space += 60;
        }
        


    }
}
