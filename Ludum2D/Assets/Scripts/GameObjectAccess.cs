using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectAccess
{
    private static Player _player;
    
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
}
