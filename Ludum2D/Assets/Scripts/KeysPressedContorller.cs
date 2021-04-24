using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class KeysPressedContorller : MonoBehaviour
{

    private List<KeyCode> _keyToPressList;

    private int _lastTimeKeyAdded;

    private List<KeyButton> _freeLeftKeyList;
    
    private List<KeyButton> _freeRightKeyList;
    void Start()
    {
        _keyToPressList = new List<KeyCode>();
        _freeLeftKeyList = new List<KeyButton>();
        _freeRightKeyList = new List<KeyButton>();
        InitFreeKeysLists();

    }

    //48-57  97-122
    void Update()
    {
        
        //CreateKeyToPressByTime();
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
    public KeyCode RandomizeKey()
    {
        int i = Random.Range(0, _freeKeyList.Count);
        int freeKey = _freeKeyList[i].IntKey();
        _freeKeyList.RemoveAt(i);
        
        return (KeyCode) (freeKey);
    }

    public void ReleaseKeyToPress(KeyButton keyButton)
    {
        _freeKeyList.Add(keyButton);
    }

    public bool AlreadyInListToPress(KeyCode keyCode)
    {  
        return _keyToPressList.Contains(keyCode);
    }

    public void AddKeyToPress()
    {
        KeyCode keyCode = RandomizeKey();
        _keyToPressList.Add(keyCode);
    }
    
    public void AddKeyToPress(KeyCode keyCode)
    {
        _keyToPressList.Add(keyCode);
    }
    
    public void RemoveKeyToPress(KeyCode keyCode)
    {
        _keyToPressList.Remove(keyCode);
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
        foreach (KeyCode key in _keyToPressList)
        {
            GUI.Label(new Rect(space, 490, 60, 30), key + "");
            space += 60;
        }
        


    }
}
