using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysPressed : MonoBehaviour
{

    private List<KeyCode> _keyToPressList;

    private List<int> _freeKeys;

    private int _lastTimeKeyAdded;
    
    // Start is called before the first frame update
    void Start()
    {
        _keyToPressList = new List<KeyCode>();
        _freeKeys = new List<int>();
        PopulateFreeKeys();
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
    public KeyCode RandomizeKey()
    {
        int i = Random.Range(0, _freeKeys.Count);
        int freeKey = _freeKeys[i];
        _freeKeys.RemoveAt(i);
        
        return (KeyCode) (freeKey);
    }

    public void ReleaseKeyToPress(KeyCode keyCode)
    {
        int keyValue = (int) (keyCode);
        _freeKeys.Add(keyValue);
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
    
    private void PopulateFreeKeys()
    {
        for (int i = 48; i < 58; i++)
        {
            _freeKeys.Add(i);
        }
        for (int i = 97; i < 123; i++)
        {
            _freeKeys.Add(i);
        }
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
