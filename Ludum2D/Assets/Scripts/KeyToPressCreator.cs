using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyToPressCreator : MonoBehaviour
{

    public bool CreateOrDestroy;


    void Update()
    {
        

        if (GameObjectAccess.Player.transform.position.y < transform.position.y)
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
    
}
