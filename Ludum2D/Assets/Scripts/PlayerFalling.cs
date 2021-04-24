using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : MonoBehaviour
{
    public float velocity;

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * velocity * Vector3.down;
    }
}
