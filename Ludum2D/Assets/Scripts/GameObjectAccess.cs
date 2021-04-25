using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectAccess
{
    private static Player _player = null;
    private static Camera _mainCamera = null;
    private static KeysController _keysController = null;
    
    public static Player Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            }

            return _player;
        }
    }

    public static Camera MainCamera
    {
        get
        {
            if(_mainCamera == null) {
                _mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            }

            return _mainCamera;
        }
    }
    
    public static KeysController KeysController
    {
        get
        {
            if(_keysController == null) {
                _keysController = GameObject.FindWithTag("KeysController").GetComponent<KeysController>();
            }

            return _keysController;
        }
    }
}
