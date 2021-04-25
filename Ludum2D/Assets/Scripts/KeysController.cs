using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class KeysController : MonoBehaviour
{

    public int SecondsToNewKey;
    
    public int MaxKeys; //número máximo de teclas que podem ser pressionadas ao mesmo tempo
    
    private List<KeyButton> _keyToPressList;

    private int _lastTimeKeyAdded;

    private List<KeyButton> _freeLeftKeyList;
    
    private List<KeyButton> _freeRightKeyList;

    public bool TurnOnCreateByTime;
    void Start()
    {
        _keyToPressList = new List<KeyButton>();
        _freeLeftKeyList = new List<KeyButton>();
        _freeRightKeyList = new List<KeyButton>();
        MaxKeys = 6;
        InitFreeKeysLists();
    }

    //48-57  97-122
    void Update()
    {
        if(TurnOnCreateByTime){
            CreateKeyToPressByTime();//aqui
        }
        
    }

    private void CreateKeyToPressByTime()
    {
        if (TruncateTime() % SecondsToNewKey == 0 & TruncateTime() != _lastTimeKeyAdded)
        {
            _lastTimeKeyAdded = TruncateTime();
            if(_keyToPressList.Count < MaxKeys)
            {
                AddRandomKeyToPress();
            }
            else
            {
                ReleaseRandomKeyToPress();
            }
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
    
    public KeyButton RandomizeLeftKey()
    {
        int i = Random.Range(0, _freeLeftKeyList.Count);
        KeyButton freeKey = _freeLeftKeyList[i];
        _freeLeftKeyList.RemoveAt(i);
        return freeKey;
    }
    
    public KeyButton RandomizeRightKey()
    {
        int i = Random.Range(0, _freeRightKeyList.Count);
        KeyButton freeKey = _freeRightKeyList[i];
        _freeRightKeyList.RemoveAt(i);
        return freeKey;
    }

    public void ReleaseKeyToPress(KeyButton keyButton)
    {
        if (_keyToPressList.Contains(keyButton))
        {
            _keyToPressList.Remove(keyButton);
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
    
    public void ReleaseRandomKeyToPress()
    {
        if (_keyToPressList.Count > 0)
        {
            int i = Random.Range(0, _keyToPressList.Count);
            KeyButton freeKey = _keyToPressList[i];
            ReleaseKeyToPress(freeKey);
        }

    }
    

    public void AddRandomKeyToPress()
    {
        KeyButton key = RandomizeKey();
        key.SetToPress();
        _keyToPressList.Add(key);
    }


    
    public KeyButton AddRandomKeyToBubble(bool isLeft)
    {
        KeyButton key =  isLeft ? RandomizeLeftKey() :  RandomizeRightKey();
        _keyToPressList.Add(key);
        return key;
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
    

}
