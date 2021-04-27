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
        float playerDistance = Mathf.Abs(GameObjectAccess.Player.transform.position.y - transform.position.y);
        if (IsSwimUp & GameObjectAccess.Player.GoingDirection() == 1)
        {

            if (GameObjectAccess.Player.transform.position.y > transform.position.y && playerDistance <= 1)
            {
                Execute();
            }
        }
        else if(!IsSwimUp & GameObjectAccess.Player.GoingDirection() == -1)
        {
            if (GameObjectAccess.Player.transform.position.y < transform.position.y && playerDistance <= 1)
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
