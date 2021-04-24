using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChange : MonoBehaviour
{
    
    public Camera MainCamera;
    public Transform PlayerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPosition.position.y < -5)
        {
            MainCamera.backgroundColor = Color.green;
        }

    }
}
