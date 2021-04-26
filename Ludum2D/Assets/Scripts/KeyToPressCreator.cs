using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyToPressCreator : MonoBehaviour
{

    public bool CreateOrDestroy;
    public bool IsSwimUp;

    void Update()
    {
        if (IsSwimUp & GameObjectAccess.Player.GoingDirection() == 1)
        {
            if (GameObjectAccess.Player.transform.position.y > transform.position.y)
            {
                Execute();
            }
        }else if(!IsSwimUp & GameObjectAccess.Player.GoingDirection() == -1)
        {
            if (GameObjectAccess.Player.transform.position.y < transform.position.y)
            {
                Execute();
            }
        }

        
    }

    private void Execute()
    {
        
            if (!CreateOrDestroy)
            {
                GameObjectAccess.KeysController.AddRandomKeyToPress();
                Destroy(this);
            }
            else
            {
                GameObjectAccess.KeysController.ReleaseRandomKeyToPress();
                Destroy(this);
            }

        
    }

}
