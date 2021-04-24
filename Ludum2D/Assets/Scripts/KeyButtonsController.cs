using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyButtonsController : MonoBehaviour
{
    public GameObject ButtonPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
